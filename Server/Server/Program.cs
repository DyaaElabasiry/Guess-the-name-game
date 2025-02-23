using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api_Messages;


class Server
{
    static TcpListener listener;
    static Dictionary<string, Room> rooms = new Dictionary<string, Room>();

    public static async Task Start()
    {
        listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server started...");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("New client connected.");
            Task.Run(() => { HandleClient(client); });
        }
    }

    static async Task HandleClient(TcpClient client)
    {
        Player player = new Player() { client = client };
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        
        /// automatic join to test the last page of the game
        /*player.roomId = "Room1";
        if (!rooms.ContainsKey(player.roomId))
        {
            rooms[player.roomId] = new Room();
        }

        rooms[player.roomId].roomId = player.roomId;
        rooms[player.roomId].category = GameCategory.animal;
        rooms[player.roomId].word = "CAT";
        if (rooms[player.roomId].player1 == null)
        {
            rooms[player.roomId].player1 = player;
        }
        else
        {
            rooms[player.roomId].player2 = player;
        }*/


        try
        {
            while (true)
            {
                
                
                Request request = await GetRequest(player);


                if (request.Type == RequestType.login)
                {
                    Console.WriteLine("Login request");
                    loginRequestPayload payload =
                        JsonSerializer.Deserialize<loginRequestPayload>(request.payload.ToString());
                    player.username = payload.username;
                }
                else if (request.Type == RequestType.create)
                {
                    Console.WriteLine("Create request");
                    createRoomRequestPayload payload =
                        JsonSerializer.Deserialize<createRoomRequestPayload>(request.payload.ToString());
                    player.roomId = payload.roomId;
                    rooms[player.roomId] = new Room();
                    rooms[player.roomId].roomId = player.roomId;
                    rooms[player.roomId].category = payload.category;
                    rooms[player.roomId].word = GetRandomWord(payload.category);
                    rooms[player.roomId].guessedChars = new string('_', rooms[player.roomId].word.Length);
                    //rooms[player.roomId].players.Add(player);
                }
                else if (request.Type == RequestType.join)
                {
                    Console.WriteLine("join request");
                    joinRequestPayload payload =
                        JsonSerializer.Deserialize<joinRequestPayload>(request.payload.ToString());
                    string roomId = payload.roomId;
                    player.roomId = roomId;
                    if(rooms[roomId].player1 == null)
                    {
                        rooms[roomId].player1 = player;
                    }
                    else
                    {
                        rooms[roomId].player2 = player;
                    }

                    
                }
                else if (request.Type == RequestType.getRooms)
                {
                    Console.WriteLine("get rooms request");
                    List<RoomInfo> roomsInfo = new List<RoomInfo>();
                    foreach (var room in rooms)
                    {
                        int count = 0;
                        if (room.Value.player1 != null)
                        {
                            count++;
                        }
                        if (room.Value.player2 != null)
                        {
                            count++;
                        }
                        RoomInfo roomInfo = new RoomInfo
                        {
                            roomId = room.Value.roomId,
                            category = room.Value.category,
                            word = room.Value.word,
                            numberOfPlayers = count
                        };
                        roomsInfo.Add(roomInfo);
                        
                        
                    }

                    getRoomsResponsePayload payload = new getRoomsResponsePayload
                        { rooms = roomsInfo};
                    Response response = new Response
                    {
                        Type = ResponseType.getRooms,
                        payload = JsonSerializer.SerializeToElement(payload)
                    };
                    SendResponse(player, response);
                }
                else if (request.Type == RequestType.getUserName)
                {
                    Console.WriteLine("get userName request");
                    getUserNameResponsePayload payload = new getUserNameResponsePayload { username = player.username };
                    Response response = new Response
                    {
                        Type = ResponseType.getUserName,
                        payload = JsonSerializer.SerializeToElement(payload)
                    };
                    SendResponse(player, response);
                }
                else if (request.Type == RequestType.gameOver)
                {
                    Console.WriteLine("gameOver request");
                    Player otherPlayer = GetOtherPlayer(player);
                    
                    Response response = new Response
                    {
                        Type = ResponseType.gameOver
                    };
                    SendResponse(otherPlayer, response);
                }
                else if (request.Type == RequestType.pressedKey)
                {
                    Console.WriteLine("pressedKey request");
                    pressedKeyRequestPayload payload =
                        JsonSerializer.Deserialize<pressedKeyRequestPayload>(request.payload.ToString());
                    Player otherPlayer = GetOtherPlayer(player);
                    
                    rooms[player.roomId].guessedChars = payload.guessedChars;
                    Console.WriteLine($"guessed word {payload.guessedChars}");
                    //
                    yourTurnResponsePayload yourTurnPayload = new yourTurnResponsePayload
                    {
                        key = payload.key,
                        guessedChars = payload.guessedChars
                    };
                    Response response = new Response
                    {
                        Type = ResponseType.yourTurn,
                        payload = yourTurnPayload
                        
                    };
                    BroadCastToSpectators(player,response);
                    SendResponse(otherPlayer, response);
                }
                else if(request.Type == RequestType.spectate)
                {
                    Console.WriteLine("spectate request");
                    spectateRequestPayload requestPayload =
                        JsonSerializer.Deserialize<spectateRequestPayload>(request.payload.ToString());
                    string roomId = requestPayload.roomId;
                    Room room = rooms[roomId];
                    player.roomId = roomId;
                    room.spectators.Add(player);
                    spectateRoomResponsePayload responsePayload = new spectateRoomResponsePayload()
                    {
                        roomInfo = new RoomInfo()
                        {
                            category = room.category,
                            guessedChars = room.guessedChars,
                            player1Name = room.player1.username,
                            player2Name = room.player2.username,
                            roomId = room.roomId,
                            roomName = room.roomId,
                            word = room.word
                        }
                    };
                    Response response = new Response
                    {
                        Type = ResponseType.spectateRoom,
                        payload = responsePayload
                    };
                    SendResponse(player, response);
                }
                else

                {
                    
                }
            }

            Console.WriteLine("Client disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.Source);
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            /*client.Close();
            if (roomName != null)
            {
                rooms[int.Parse(roomName)].Remove(client);
            }*/
        }
    }

    static async Task SendResponse(Player player, Response response)
    {
        Console.WriteLine($"Sending response: {response.Type.ToString()} to {player.username}");
        string message = JsonSerializer.Serialize(response);
        byte[] data = Encoding.UTF8.GetBytes(message);
        player.client.GetStream().WriteAsync(data, 0, data.Length);
    }

    static async Task<Request> GetRequest(Player player)
    {
        byte[] buffer = new byte[1024];
        int bytesRead = await player.client.GetStream().ReadAsync(buffer, 0, buffer.Length);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        return JsonSerializer.Deserialize<Request>(message);
    }
    static string GetRandomWord(GameCategory category)
    {
        string[] words = new string[3];
        if (category == GameCategory.animal)
        {
            words = new string[] { "CAT", "DOG", "LION" };
        }
        else if (category == GameCategory.food)
        {
            words = new string[] { "PIZZA", "BURGER", "SANDWICH" };
        }
        else if (category == GameCategory.country)
        {
            words = new string[] { "TURKEY", "GERMANY", "FRANCE" };
        }

        Random random = new Random();
        return words[random.Next(0, words.Length)];
    }
    static Player? GetOtherPlayer(Player player)
    {
        if (rooms[player.roomId].player1 == player)
        {
            return rooms[player.roomId].player2;
        }
        else
        {
            return rooms[player.roomId].player1;
        }
    }
    static void BroadCastToSpectators(Player player, Response response)
    {
        foreach (var spectator in rooms[player.roomId].spectators)
        {
            SendResponse(spectator, response);
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        await Server.Start();
    }
}
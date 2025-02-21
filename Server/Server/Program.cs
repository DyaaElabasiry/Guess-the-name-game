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
            Task.Run(() => { HandleClient(client);}) ;
        }
    }
    static  async Task HandleClient(TcpClient client)
    {
        Player player = new Player(){client = client};
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        try
        {
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;  // Client disconnected

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {message}");
                
                
                var request = JsonSerializer.Deserialize<Request>(message);
                
                
                if (request.Type == RequestType.login)
                {
                    Console.WriteLine("Login request");
                    loginRequestPayload payload = JsonSerializer.Deserialize<loginRequestPayload>(request.payload.ToString());
                    player.username = payload.username;
                    
                    
                }
                else if (request.Type == RequestType.create)
                {
                    Console.WriteLine("Create request");
                    createRoomRequestPayload payload = JsonSerializer.Deserialize<createRoomRequestPayload>(request.payload.ToString());
                    player.roomId = payload.roomId;
                    rooms[player.roomId] = new Room();
                    rooms[player.roomId].roomId = player.roomId;
                    //rooms[player.roomId].players.Add(player);
                }
                else if (request.Type == RequestType.join)
                {
                    Console.WriteLine("join request");
                    joinRequestPayload payload = JsonSerializer.Deserialize<joinRequestPayload>(request.payload.ToString());
                    player.roomId = payload.roomId;
                    rooms[player.roomId].players.Add(player);
                }
                else if (request.Type == RequestType.getRooms)
                {
                    Console.WriteLine("get rooms request");
                    List<string> roomIds = new List<string>();
                    List<GameCategory> categories = new List<GameCategory>();
                    foreach (var room in rooms)
                    {
                        roomIds.Add(room.Value.roomId);
                        categories.Add(room.Value.category);
                    }
                    getRoomsResponsePayload payload = new getRoomsResponsePayload{roomIds = roomIds, categories = categories};
                    Response response = new Response
                    {
                        Type = ResponseType.getRooms,
                        payload = JsonSerializer.SerializeToElement(payload)
                    };
                    await SendResponse(player, response);
                }
                else if(request.Type == RequestType.getUserName)
                {
                    Console.WriteLine("get userName request");
                    getUserNameResponsePayload payload = new getUserNameResponsePayload{username = player.username};
                    Response response = new Response
                    {
                        Type = ResponseType.getUserName,
                        payload = JsonSerializer.SerializeToElement(payload)
                    };
                    await SendResponse(player, response);
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
        string message = JsonSerializer.Serialize(response);
        byte[] data = Encoding.UTF8.GetBytes(message);
        await player.client.GetStream().WriteAsync(data, 0, data.Length);
    }
    static async Task<Response> GetResponse(Player player)
    {
        byte[] buffer = new byte[1024];
        int bytesRead = await player.client.GetStream().ReadAsync(buffer, 0, buffer.Length);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        return JsonSerializer.Deserialize<Response>(message);
    }
    
    
}

class Program
{
    
    static async Task Main(string[] args)
    {
        await Server.Start();
    }
    
}
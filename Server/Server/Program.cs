using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api_Messages;


public class Player
{
    public string username { get; set; }
    public TcpClient client { get; set; }
    public int roomId { get; set; }
    
}


class Server
{
    static TcpListener listener;
    static Dictionary<int, List<Player>> rooms = new Dictionary<int, List<Player>>();

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
                
                
                if (request.requestType == RequestType.login)
                {
                    loginRequestPayload payload = JsonSerializer.Deserialize<loginRequestPayload>(request.payload.ToString());
                    player.username = payload.username;
                    Console.WriteLine("Login request");
                    
                    
                }
                else if (request.requestType == RequestType.create)
                {
                    createRequestPayload payload = JsonSerializer.Deserialize<createRequestPayload>(request.payload.ToString());
                    Console.WriteLine("Create request");
                    player.roomId = payload.roomId;
                    rooms[player.roomId] = new List<Player>();
                    rooms[player.roomId].Add(player);
                }
                else if (request.requestType == RequestType.join)
                {
                    joinRequestPayload payload = JsonSerializer.Deserialize<joinRequestPayload>(request.payload.ToString());
                    player.roomId = payload.roomId;
                    rooms[player.roomId].Add(player);
                }
                else if (request.requestType == RequestType.getRooms)
                {
                    Console.WriteLine("get rooms request");
                    List<int> roomIds = rooms.Keys.ToList();
                    listResponsePayload payload = new listResponsePayload{roomIds = roomIds};
                    Response response = new Response
                    {
                        responseType = ResponseType.getRooms,
                        payload = JsonSerializer.SerializeToElement(payload)
                    };
                    await SendResponse(player, response);
                }
                else if(request.requestType == RequestType.getUserName)
                {
                    getUserNameResponsePayload payload = new getUserNameResponsePayload{username = player.username};
                    Response response = new Response
                    {
                        responseType = ResponseType.getUserName,
                        payload = JsonSerializer.SerializeToElement(payload)
                    };
                    await SendResponse(player, response);
                }
                
                
            }
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
    
    
}

class Program
{
    
    static async Task Main(string[] args)
    {
        await Server.Start();
    }
    
}
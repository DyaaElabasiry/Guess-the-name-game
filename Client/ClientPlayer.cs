using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api_Messages;

namespace Client
{
    class ClientPlayer
    {
        public static TcpClient client;
        public static string PlayerName;
        public static RoomInfo roomInfo = new RoomInfo() ;
        
        public static  async Task SendRequest(Request request)
        {
            string message = JsonSerializer.Serialize(request);
            byte[] data = Encoding.UTF8.GetBytes(message);
            await client.GetStream().WriteAsync(data, 0, data.Length);
        }
        public static async Task<Response> GetResponse()
        {
            byte[] data = new byte[1024];
            int bytes = await client.GetStream().ReadAsync(data, 0, data.Length);
            string message = Encoding.UTF8.GetString(data, 0, bytes);
            return JsonSerializer.Deserialize<Response>(message);
        }
    }
}

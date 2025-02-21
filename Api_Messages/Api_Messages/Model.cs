using System.Net.Sockets;

namespace Api_Messages;

public class Player
{
    public string username { get; set; }
    public TcpClient client { get; set; }
    public string roomId { get; set; }
    
}
public class Room
{
    public string roomId { get; set; }
    public string roomName { get; set; }
    public GameCategory category { get; set; }
    public List<Player> players = new List<Player>(); 
    public List<Player> spectators = new List<Player>();
}

public enum GameCategory
{
    animal,
    food,
    country
}
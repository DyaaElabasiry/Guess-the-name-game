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
    public string word { get; set; }
    public string guessedChars { get; set; }
    public bool isReady = false;
    public Player? player1 = null;
    public Player? player2 = null;
    //public List<Player> players = new List<Player>(); 
    public List<Player> spectators = new List<Player>();
}

/// <summary>
///  contains every information about the room exept the players and spectators objects
/// </summary>
public class RoomInfo
{
    public string word { get; set; }
    public string guessedChars { get; set; }
    public string roomId { get; set; }
    public string roomName { get; set; }
    public string player1Name { get; set; }
    public string player2Name { get; set; }
    public bool isReady { get; set; }
    public GameCategory category { get; set; }
    public int numberOfPlayers { get; set; }
}

public enum GameCategory
{
    animal,
    food,
    country
}
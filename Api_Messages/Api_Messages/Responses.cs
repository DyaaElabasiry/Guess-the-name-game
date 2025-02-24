namespace Api_Messages;


public enum ResponseType
{
    list,login,getUserName,getRooms,spectateRoom,yourTurn,gameOver,startGame
}

public class Response
{
    public ResponseType Type { get; set; }
    public object payload  { get; set; }
}

public class getUserNameResponsePayload
{
    public string username { get; set; }
    
}
public class getRoomsResponsePayload
{
    /*public List<string> roomIds { get; set; }
    public List<GameCategory> categories { get; set; }
    public List<int> numberOfPlayers { get; set; }*/
    public List<RoomInfo> rooms { get; set; }
}
/// <summary>
/// //////
/// </summary>
public class spectateRoomResponsePayload
{
    
    public RoomInfo roomInfo { get; set; }
    
}
public class yourTurnResponsePayload
{
    public char  key { get; set; }
    // to update the guessedChars in the server side
    public string guessedChars { get; set; }
    
}


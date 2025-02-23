namespace Api_Messages;


public enum RequestType
{
    login,
    spectate,
    join,
    getUserName,
    list,
    create,
    getRooms,
    move,
    pressedKey,
    gameOver,
    
}


public class Request
{
    public RequestType Type { get; set; }
    public object payload  { get; set; }
}





public class loginRequestPayload
{
    public string username { get; set; }

    
}

public class createRoomRequestPayload
{
    public string roomId { get; set; }
    public GameCategory category { get; set; }

    
}
public class joinRequestPayload
{
    public string roomId { get; set; }
    
}
public class spectateRequestPayload
{
    public string roomId { get; set; }
    
}
public class pressedKeyRequestPayload
{
    public char key { get; set; }
    public string guessedChars { get; set; }
}
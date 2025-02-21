namespace Api_Messages;


public enum RequestType
{
    login,
    getUserName,
    list,
    create,
    getRooms,
    join,
    move
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
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
    public RequestType requestType { get; set; }
    public object payload  { get; set; }
}





public class loginRequestPayload
{
    public string username { get; set; }

    
}

public class createRequestPayload
{
    public int roomId { get; set; }

    
}
public class joinRequestPayload
{
    public int roomId { get; set; }
    
}
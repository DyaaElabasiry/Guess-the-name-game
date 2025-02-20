namespace Api_Messages;


public enum ResponseType
{
    list,login,getUserName,getRooms
}

public class Response
{
    public ResponseType responseType { get; set; }
    public object payload  { get; set; }
}
public class listResponsePayload
{
    public List<int> roomIds { get; set; }
}
public class getUserNameResponsePayload
{
    public string username { get; set; }
    
}
public class getRoomsResponsePayload
{
    public List<int> roomIds { get; set; }
    
}

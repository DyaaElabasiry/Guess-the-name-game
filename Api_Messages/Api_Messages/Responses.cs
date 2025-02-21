namespace Api_Messages;


public enum ResponseType
{
    list,login,getUserName,getRooms,roomInfo,yourTurn,gameOver
}

public class Response
{
    public ResponseType Type { get; set; }
    public object payload  { get; set; }
}
/*public class listResponsePayload
{
    public List<string> roomIds { get; set; }
    public List<GameCategory> categories { get; set; }
}*/
public class getUserNameResponsePayload
{
    public string username { get; set; }
    
}
public class getRoomsResponsePayload
{
    public List<string> roomIds { get; set; }
    public List<GameCategory> categories { get; set; }
}
/// <summary>
/// //////
/// </summary>
public class roomInfoResponsePayload
{
    
    public string word { get; set; }
    public string otherPlayerName { get; set; }
    
}



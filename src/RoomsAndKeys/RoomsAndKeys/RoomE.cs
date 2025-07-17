namespace RoomsAndKeys;

public class RoomE
{
    public string key = "Room has key B";

    public RoomE(string key)
    {
        if (key != null)
        {
            key = RoomD.key;
        }
    }
}

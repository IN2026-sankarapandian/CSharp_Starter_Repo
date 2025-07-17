namespace RoomsAndKeys;

public class RoomC : RoomB
{
    //public string key = "Room has key C";

    public RoomC(string key1, string key2) : base(key2)
    {
        this.key = key1;
    }

}

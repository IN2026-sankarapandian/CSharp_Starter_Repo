using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace RoomsAndKeys;

public class RoomA
{
    public string key = "Room has no key";

    public RoomA(string key)
    {
        if(key == null)
        {
            key = RoomD.key;
        }
    }
}

namespace RoomsAndKeys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoomA roomB = new RoomA("value");
            RoomB roomC = new RoomC("Room has no key", "Room has key A");
            if(roomC.key.Equals(RoomD.key))
            {
                roomB.key = RoomD.key;
            }
            Console.WriteLine(roomB.key);
            Console.WriteLine(RoomD.key);
            Console.WriteLine(roomC.key);
            Console.ReadKey();

        }
    }
}
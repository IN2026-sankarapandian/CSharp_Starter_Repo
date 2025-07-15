namespace ConsoleApp2
{
    class ListManager
    {

        private static List<int> _integers = new List<int> { 1, 2, 3, 4, 5 };
        public static void Print()
        {
            foreach (var integer in _integers)
            {
                Console.WriteLine(integer);
            }
        }
        public
    }
}

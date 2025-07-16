namespace dcs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();
            Console.WriteLine("Hiiiii");
            c.Increment();
            c.print();
            Console.ReadKey();
        }
    }
    class Counter
    {
        public int Count;
        public void Increment()
        {
            int k = Count++;
            Console.WriteLine(k);
        }

        public void print()
        {
            Console.WriteLine("Hello");
        }
    }
}

using System;

namespace SingletonWithLazyInstantiation
{
    // Below is the lazy implementation of singleton pattern without lock object.

    public sealed class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        static Singleton()
        {
            Console.WriteLine("Static constructor for singleton object is called");
        }

        private Singleton()
        { }

        public static Singleton Instance
        {
            get
            {
                return Singleton.instance;
            }
        }

        public string DoSomething()
        {
            return "Singleton with lazy instantiation implemented.";
        }

        public static void DoTest()
        {
            Console.WriteLine("DoTest called. This is not so lazy instantiation");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Singleton.DoTest();
            Singleton singleton = Singleton.Instance;
            Console.WriteLine(singleton.DoSomething());
            Console.ReadKey();
        }
    }
}
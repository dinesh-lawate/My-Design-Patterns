using System;

namespace SingletonWithFullyLazyInstantiation
{
    // Below is the fully lazy implementation of singleton pattern without lock object.

    public sealed class Singleton
    {
        private class SingletonHolder
        {
            internal static readonly Singleton instance = new Singleton();

            static SingletonHolder()
            {
                Console.WriteLine("Static constructor for singleton object is called");
            }
        }

        private Singleton()
        { }

        public static Singleton Instance
        {
            get
            {
                return SingletonHolder.instance;
            }
        }

        public string DoSomething()
        {
            return "Singleton with fully lazy instantiation implemented.";
        }

        public static void DoTest()
        {
            Console.WriteLine("DoTest called. This is fully lazy instantiation");
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
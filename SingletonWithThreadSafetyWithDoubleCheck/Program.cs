using System;

namespace SingletonWithThreadSafetyWithDoubleCheck
{
    // Below is the thread safe with double check implementation of singleton pattern with lock object.

    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly Object lockObject = new Object();

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                    }
                }

                return instance;
            }
        }

        public string DoSomething()
        {
            return "Singleton with thread safety with double check implemented.";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Singleton singleton = Singleton.Instance;
            Console.WriteLine(singleton.DoSomething());
            Console.ReadKey();
        }
    }
}
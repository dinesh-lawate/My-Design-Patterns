using System;

namespace SingletonWithoutThreadSafety
{
    //First of all Thanks to Jon Skeet for letting us know the different implementation of Singleton pattern.
    //Please do read this article by Jon Skeet - https://csharpindepth.com/articles/Singleton

    //Singleton pattern definition - Essentially, a singleton is a class which only allows a single instance of itself to be created,
    //and usually gives simple access to that instance.

    //In singleton pattern we will have always have sealed class, private constructor, private static singleton instance and
    //public static method which will return singleton instance.

    // Below is the non thread safe implementation of singleton pattern.
    public sealed class Singleton
    {
        private static Singleton instance = null;

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

        public string DoSomething()
        {
            return "Singleton without thread safety implemented.";
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
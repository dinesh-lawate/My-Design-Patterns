using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSub
{
    /*
        Defination - The PubSub pattern is a software design pattern in which an object, called the publisher 
        notifies subscriber automatically of any state changes. Here publisher doesn't know any thing about the subscriber.        
    */
    class Program
    {
        static void Main(string[] args)
        {
            IStock microsoftStock = new Stock("Microsoft");

            IInvestor investor1 = new Investor("Adam");
            IInvestor investor2 = new Investor("John");

            investor1.Subscribe(microsoftStock);
            investor2.Subscribe(microsoftStock);

            microsoftStock.StockPrice = 100;

            double newPrice = Convert.ToDouble(Console.ReadLine());

            microsoftStock.StockPrice = newPrice;

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Event argument type passed from publisher to subcriber
    /// </summary>
    public class StockEventArgs : EventArgs
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public StockEventArgs(string name, double value)
        {
            this.Name = name;
            this.Price = value;
        }
    }

    /// <summary>
    /// Abstract publisher
    /// </summary>
    public interface IStock
    {
        string StockName { get; }
        double StockPrice { get; set; }

        event EventHandler<StockEventArgs> OnPriceChange;
    }

    /// <summary>
    /// Concrete publisher
    /// </summary>
    public class Stock : IStock
    {
        private double _stockPrice;

        public Stock(string name)
        {
            this.StockName = name;
        }

        /// <summary>
        /// Exposes price change event for subscribers
        /// </summary>
        public event EventHandler<StockEventArgs> OnPriceChange;

        public double StockPrice
        {
            get { return _stockPrice; }
            set
            {
                _stockPrice = value;
                OnPriceChange(this, new StockEventArgs(StockName, value));
            }
        }

        public string StockName { get; }
    }

    /// <summary>
    /// Abstract Subscriber
    /// </summary>
    interface IInvestor
    {
        void Subscribe(IStock stock);
    }

    /// <summary>
    /// Concrete Subscriber
    /// </summary>
    class Investor : IInvestor
    {
        public string Name { get; set; }

        public Investor(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Subscribing to price change event
        /// </summary>
        /// <param name="stock"></param>
        public void Subscribe(IStock stock)
        {
            Console.WriteLine("Investor {0} subscribed to stock {1}.", this.Name, stock.StockName);
            
            stock.OnPriceChange += (sender, e) =>
            {
                Console.WriteLine("Hello Mr. {0}, Price of {1} changed to {2}.", Name, stock.StockName, stock.StockPrice);
            };
        }
    }
}

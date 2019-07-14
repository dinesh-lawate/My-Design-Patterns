using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    /*
        Defination - The observer pattern is a software design pattern in which an object, called the subject, 
        maintains a list of its dependents, called observers, and notifies them automatically of any state changes, 
        usually by calling one of their methods.
        This pattern is used when there is one too many relationships between objects such as if one object is modified, 
        its dependent objects are to be notified automatically.
    */
    class Program
    {
        static void Main(string[] args)
        {
            NasdaqStock stock = new NasdaqStock("Microsoft", 100);

            Investor investor1 = new Investor("Adam");
            Investor investor2 = new Investor("John");

            stock.Attach(investor1);
            stock.Attach(investor2);

            stock.Price = 101;
            stock.Price = 110;

            Console.ReadKey();
        }

        /// <summary>
        /// Abstract Subject
        /// </summary>
        abstract class Stock
        {
            private List<IInvestor> _investors = new List<IInvestor>();
            private string _stockName;
            private double _stockPrice;

            public Stock(string name, double price)
            {
                this._stockName = name;
                this._stockPrice = price;
            }

            public IInvestor Attach(IInvestor investor)
            {
                this._investors.Add(investor);
                Console.WriteLine("Investor {0} subscribed to stock {1}.", (investor as Investor).Name, this.Name);
                return investor;
            }

            public void Detach(IInvestor investor)
            {
                this._investors.Remove(investor);
            }

            public void Notify()
            {
                foreach (var investor in this._investors)
                {
                    investor.Update(this);
                }
            }

            public double Price
            {
                get { return this._stockPrice; }
                set
                {
                    if (this._stockPrice != value)
                    {
                        this._stockPrice = value;
                        Notify();
                    }
                }
            }

            public string Name
            {
                get { return this._stockName; }
                set { this._stockName = value; }
            }
        }

        /// <summary>
        /// Concrete Subject
        /// </summary>
        class NasdaqStock : Stock
        {
            public NasdaqStock(string name, double price) : base(name, price)
            {
            }
        }

        /// <summary>
        /// Abstract Observer
        /// </summary>
        interface IInvestor
        {
            void Update(Stock stock);
        }

        /// <summary>
        /// Concrete Observer
        /// </summary>
        class Investor : IInvestor
        {
            public string Name { get; set; }

            public Investor(string name)
            {
                Name = name;
            }

            public void Update(Stock stock)
            {
                Console.WriteLine("Hello Mr. {0}, Price of {1} changed to {2}.", Name, stock.Name, stock.Price);
            }
        }
    }
}

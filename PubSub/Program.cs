using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSub
{
    public class StatusUpdateEventArgs : EventArgs
    {
        public string Value { get; set; }

        public StatusUpdateEventArgs(string value)
        {
            this.Value = value;
        }
    }

    public class StatusUpdatePublisher
    {
        public event EventHandler<StatusUpdateEventArgs> onStatusUpdate = delegate (Object sender, StatusUpdateEventArgs e)
        {
        };

        public virtual void Publish(string status)
        {
            onStatusUpdate(this, new StatusUpdateEventArgs(status));
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            StatusUpdatePublisher statusUpdatePublisher = new StatusUpdatePublisher();

            statusUpdatePublisher.onStatusUpdate += (sender, e) =>
            {
                Console.WriteLine("Subscriber 1 " + e.Value);
            };

            statusUpdatePublisher.Publish("New");

            statusUpdatePublisher.onStatusUpdate += (sender, e) =>
            {
                Console.WriteLine("Subscriber 2 " + e.Value);
            };

            string newStatus = Console.ReadLine();

            statusUpdatePublisher.Publish(newStatus);

            Console.ReadKey();

        }
    }
}

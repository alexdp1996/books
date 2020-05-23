using RabbitMQ;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var wrapper = new RabbitMQWrapper())
            {
                wrapper.Consume("Books", GetQueueHandler("Books"));
                wrapper.Consume("Authors", GetQueueHandler("Authors"));
                Console.WriteLine("Press enter to quit.");
                Console.ReadLine();
            }
        }

        static EventHandler<BasicDeliverEventArgs> GetQueueHandler(string queue)
        {
            return (obj, args) =>
            {
                var body = args.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine("{0} {1}: {2}", queue, DateTime.Now, message);
            };
        }
    }
}

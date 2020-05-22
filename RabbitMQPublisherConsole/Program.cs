using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQPublisherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Books");
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += RecievedHandler;
                Console.WriteLine("Press enter to quit.");
                Console.ReadLine();
            }
        }

        static void RecievedHandler(object obj, BasicDeliverEventArgs args)
        {
            var body = args.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine("{0}: {1}", DateTime.Now, message);

        }
    }
}

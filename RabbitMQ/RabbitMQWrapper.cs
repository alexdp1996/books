using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace RabbitMQ
{
    public class RabbitMQWrapper : IDisposable
    {
        private IConnection Connection { get; }
        private List<IModel> Channels { get; }

        public RabbitMQWrapper()
        {
            var factory = new ConnectionFactory { HostName = ConfigurationManager.AppSettings["RabbitMQHost"] };
            Connection = factory.CreateConnection();
            Channels = new List<IModel>();
        }

        public void Consume(string queue, EventHandler<BasicDeliverEventArgs> handler)
        {
            var channel = Connection.CreateModel();
            channel.QueueDeclare(queue: queue, exclusive: false, durable: true, autoDelete: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += handler;
            channel.BasicConsume(queue, true, consumer);
            Channels.Add(channel);
        }

        public void SendMessage(string exchange, string routingKey, byte[] data)
        {
            using (var channel = Connection.CreateModel())
            {
                channel.BasicPublish(exchange: exchange, routingKey: routingKey, body: data); 
            }
        }

        public void Dispose()
        {
            foreach (var c in Channels)
            {
                c.Dispose();
            }
            Connection.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}

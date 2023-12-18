using System;
using System.Text;
using Client;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Server
{
    public class Program
    {
        public class MessageContent
        {
            public string Sender { get; set; }
            public string Recipient { get; set; }
            public string? Text { get; set; }
            public byte[]? ImageData { get; set; }
        }

        public class Server
        {
            private readonly IConnection _connection;
            private readonly IModel _channel;
            private readonly string _exchangeName = "direct_messages";
            private readonly string _publicExchangeName = "fanout_exchange";

            public Server()
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };

                // Establish a RabbitMQ connection and channel. You may want to handle
                // connection failure and retry connection here.
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
                _channel.ExchangeDeclare(exchange: _publicExchangeName, type: ExchangeType.Fanout);
            }

            public void Start()
            {
                var serverQueueName = _channel.QueueDeclare().QueueName;
                // The server's queue name should be bound to the exchange so that the server can receive
                // messages from various senders. The routing key should be a key known by the senders.
                _channel.QueueBind(queue: serverQueueName,
                                  exchange: _exchangeName,
                                  routingKey: "server_routing_key");

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);
                    var messageContent = JsonConvert.DeserializeObject<MessageContent>(messageJson);
                    Console.WriteLine($"Received message from {messageContent.Sender} intended for {messageContent.Recipient}");
                    ForwardMessageToRecipient(messageContent);
                };
                _channel.BasicConsume(queue: serverQueueName,
                                     autoAck: true,
                                     consumer: consumer);
            
                Console.WriteLine("Server is started. Press [enter] to exit.");
                Console.ReadLine();
            }

            private void ForwardMessageToRecipient(MessageContent messageContent)
            {
                var routingKey = messageContent.Recipient;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));
                if(routingKey == "Everyone")
                {
                    _channel.BasicPublish(exchange: _publicExchangeName,
                        routingKey: "",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine($"Forwarded message to Everyone");
                }
                else
                {
                    _channel.BasicPublish(exchange: _exchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body);
                    Console.WriteLine($"Forwarded message to {routingKey}");    
                }
            }

            public void Stop()
            {
                _channel?.Close();
                _connection?.Close();
            }
    }

        public static void Main(string[] args)
        {
            var server = new Server();
            server.Start();
            server.Stop();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Client
{
    public class MessageContent
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string? Text { get; set; }
        public byte[]? ImageData { get; set; }
    }
    public partial class Form1 : MaterialForm
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly string _exchangeName = "direct_messages";
        private readonly string _publicExchangeName = "fanout_exchange";
        private string _clientRoutingKey; // Unique key for this client.
        private delegate void UpdateMessageDelegate(string message);
        public Form1()
        {
            InitializeComponent();
            DotNetEnv.Env.TraversePath().Load();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void login(object sender, EventArgs e)
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_DB_CONN"));
            var database = client.GetDatabase("Esercitazioni");
            var collection = database.GetCollection<BsonDocument>("Utenti");
            var filter = Builders<BsonDocument>.Filter.Eq("Username", materialTextBox2.Text);
            var result = collection.Find(filter).ToList();
            if (result.Count == 0)
            {
                MessageBox.Show("Utente non trovato");
            }
            else
            {
                var password = result[0].GetValue("Password");
                if (password == materialTextBox3.Text)
                {
                    _clientRoutingKey = materialTextBox2.Text;
                    materialLabel3.Text = materialTextBox2.Text;
                    SetupRabbitMQ();
                    SubscribeToServerMessages();
                    MessageBox.Show("Login effettuato");
                }
                else
                {
                    MessageBox.Show("Password errata");
                }
            }
        }
        // {
        //     var settings = MongoClientSettings.FromConnectionString(connectionUri);
        //     settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        //     var client = new MongoClient(settings);
        //     try {
        //         var result = client.GetDatabase("Esercitazioni").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        //         Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        //     } catch (Exception ex) {
        //         Console.WriteLine(ex);
        //     }
        // }
        
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessageToServer(materialTextBox1.Text);
            materialTextBox1.Clear();
        }

        private void SendMessageToServer(string text)
        {
            var messageContent = new MessageContent
            {
                Sender = _clientRoutingKey,
                Recipient = materialTextBox6.Text,
                Text = text
            };
            materialListView1.Items.Add($"{messageContent.Sender}: {messageContent.Text}");
            saveDb(messageContent);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));
            _channel.BasicPublish(exchange: _exchangeName,
                                  routingKey: "server_routing_key",
                                  basicProperties: null,
                                  body: body);
        }

        private void SubscribeToServerMessages()
        {
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName,
                               exchange: _exchangeName,
                               routingKey: _clientRoutingKey);
            _channel.QueueBind(queue: queueName, 
                               exchange: "fanout_exchange",
                               routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;

            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);
            var messageContent = JsonConvert.DeserializeObject<MessageContent>(messageJson);
            saveDb(messageContent);
            Invoke(new MethodInvoker(delegate ()
            {
                // if(messageContent.Text!=null) materialListView1.Items.Add($"{messageContent.Sender}: {messageContent.Text}");
                // if(messageContent.ImageData!=null) materialListView1.Items.Add($"{messageContent.Sender}: {messageContent.ImageData}");
                if (messageContent.Text != null)
                {
                    materialListView1.Items.Add($"{messageContent.Sender}: {messageContent.Text}");
                }

                if (messageContent.ImageData != null)
                {
                    // Ensure that an ImageList is associated with your ListView.
                    if (materialListView1.LargeImageList == null)
                    {
                        materialListView1.LargeImageList = new ImageList();
                    }

                    // Set the view mode to display images.
                    materialListView1.View = View.LargeIcon;
                    using (var ms = new MemoryStream(messageContent.ImageData))
                    {
                        try
                        {
                            Image image = Image.FromStream(ms);
                            materialListView1.LargeImageList.Images.Add(image);
                            var listViewItem = new ListViewItem($"{messageContent.Sender}: [Image]", materialListView1.LargeImageList.Images.Count - 1);
                            materialListView1.Items.Add(listViewItem);
                        }
                        catch (ArgumentException ex)
                        {
                            // Handle the case where the byte array is not a valid image.
                            Console.WriteLine("Invalid image data: " + ex.Message);
                        }
                    }
                }
            }));
        }

        private void SetupRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            // Add error handling, reconnect logic, and other production considerations.
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
            _channel.ExchangeDeclare(exchange: _publicExchangeName, type: ExchangeType.Fanout);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _channel?.Close();
            _connection?.Close();
        }

        private void register(object sender, EventArgs e)
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_DB_CONN"));
            var database = client.GetDatabase("Esercitazioni");
            var collection = database.GetCollection<BsonDocument>("Utenti");
            var filter = Builders<BsonDocument>.Filter.Eq("Username", materialTextBox5.Text);
            var result = collection.Find(filter).ToList();
            if (result.Count == 0)
            {
                var document = new BsonDocument
                {
                    {"Username", materialTextBox5.Text},
                    {"Password", materialTextBox4.Text}
                };
                collection.InsertOne(document);
                MessageBox.Show("Utente registrato");
            }
            else
            {
                MessageBox.Show("Utente già registrato");
            }
        }
        private void saveDb(MessageContent message)
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_DB_CONN"));
            var database = client.GetDatabase("Esercitazioni");
            var collection = database.GetCollection<BsonDocument>("Messaggi");
            var document = new BsonDocument
            {
                {"Sender", message.Sender},
                {"Recipient", message.Recipient},
                
            };
            if (!string.IsNullOrEmpty(message.Text))
            {
                document.Add("Text", message.Text);
            }
            else if (message.ImageData != null)
            {
                document.Add("ImageData", Convert.ToBase64String(message.ImageData));
            }
            collection.InsertOne(document);
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var imagePath = openFileDialog1.FileName;
            if (System.IO.File.Exists(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                var messageContent = new MessageContent
                {
                    Sender = _clientRoutingKey,
                    Recipient = materialTextBox6.Text,
                    ImageData = imageBytes
                };
                materialListView1.View = View.LargeIcon;
                using (var ms = new MemoryStream(messageContent.ImageData))
                {
                    try
                    {
                        Image image = Image.FromStream(ms);
                        materialListView1.LargeImageList.Images.Add(image);
                        var listViewItem = new ListViewItem($"{messageContent.Sender}: [Image]", materialListView1.LargeImageList.Images.Count - 1);
                        materialListView1.Items.Add(listViewItem);
                    }
                    catch (ArgumentException ex)
                    {
                        // Handle the case where the byte array is not a valid image.
                        Console.WriteLine("Invalid image data: " + ex.Message);
                    }
                }
                materialListView1.Refresh();
                //saveDb(messageContent);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));
                _channel.BasicPublish(exchange: _exchangeName,
                            routingKey: "server_routing_key",
                            basicProperties: null,
                            body: body);
            }
            else
            {
                MessageBox.Show("File non trovato: " + imagePath);
            }
        }
        
        private void SendMessageToEveryone(string text)
        {
            var messageContent = new MessageContent
            {
                Sender = _clientRoutingKey,
                Recipient = "Everyone",
                Text = text
            };
            materialListView1.Items.Add($"{messageContent.Sender}: {messageContent.Text}");
            saveDb(messageContent);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageContent));
            _channel.BasicPublish(exchange: _exchangeName,
                routingKey: "server_routing_key",
                basicProperties: null,
                body: body);
        }
        private void materialButton5_Click(object sender, EventArgs e)
        {
            SendMessageToEveryone(materialTextBox1.Text);
            materialTextBox1.Clear();
        }
    }
}
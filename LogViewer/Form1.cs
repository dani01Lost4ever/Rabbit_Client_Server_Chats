using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogViewer
{
    public class Utente{
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            DotNetEnv.Env.TraversePath().Load();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            retrieveUsers();
            materialCheckedListBox2.Enabled = false;
        }

        private void retrieveUsers()
        {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_DB_CONN"));
            var database = client.GetDatabase("Esercitazioni");
            var collection = database.GetCollection<BsonDocument>("Utenti");
            //return all the users
            var filter = new BsonDocument();
            var result = collection.Find(filter).ToList();
            //iterate over every Utente and add it to the list
            foreach (var utente in result)
            {
                var username = utente["Username"].ToString();
                var checkbox = new MaterialCheckbox
                {
                    Text = username,
                    Checked = false 
                };
                checkbox.CheckedChanged += materialCheckbox1_CheckedChanged;
                materialCheckedListBox1.Items.Add(checkbox);
            }

        }


        private void materialCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (MaterialCheckbox) sender;
            if (checkbox.Checked)
            {
                var username = checkbox.Text;
                materialExpansionPanel1.Description = username;
                var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_DB_CONN"));
                var database = client.GetDatabase("Esercitazioni");
                //search in the table messagages for users that have username as sender and print the receiver in the material checkbox list 2
                var collection = database.GetCollection<BsonDocument>("Messaggi");
                var filter = Builders<BsonDocument>.Filter.Eq("Sender", username);
                // i need to return the unique receivers
                var uniqueUsernames = new HashSet<string>();
                var result = collection.Find(filter).ToList();
                foreach (var messaggio in result)
                {
                    var receiver = messaggio["Recipient"].ToString();
                    if (uniqueUsernames.Add(receiver))
                    {
                        var checkbox2 = new MaterialCheckbox
                        {
                            Text = receiver,
                            Checked = false
                        };
                        checkbox2.CheckedChanged += materialCheckbox2_CheckedChanged;
                        materialCheckedListBox2.Items.Add(checkbox2);
                    }
                }
                
            }
        }

        private void materialCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (MaterialCheckbox) sender;
            if (checkbox.Checked)
            {
                //find messages with sender and receiver and print them in the material list view
                materialExpansionPanel2.Collapse= true;
                materialCheckedListBox2.Enabled = false;
                
                var senderMex = materialExpansionPanel1.Description;
                var receiver = checkbox.Text;
                materialExpansionPanel2.Description = receiver;
                var client = new MongoClient(System.Environment.GetEnvironmentVariable("MONGO_DB_CONN"));
                var database = client.GetDatabase("Esercitazioni");
                var collection = database.GetCollection<BsonDocument>("Messaggi");
                var filter = Builders<BsonDocument>.Filter.Eq("Sender", senderMex) & Builders<BsonDocument>.Filter.Eq("Recipient", receiver);
                var result = collection.Find(filter).ToList();
                foreach (var messaggio in result)
                {
                    var text = "";
                    
                    
                    if (messaggio.Contains("Text") && messaggio["Text"] != null) {
                        text = messaggio["Text"].ToString();
                    }else {
                        text = "No text";
                        messaggio.Add("Text", text);
                    }
                    var item = new ListViewItem(senderMex+ " To: "+ receiver+", Message: "+text);
                    materialListView1.Items.Add(item);
                }
                materialListView1.Visible = true;
            }
        }

        private void materialExpansionPanel2_SaveClick(object sender, EventArgs e)
        {
            materialExpansionPanel2.Collapse= true;
            materialCheckedListBox2.Enabled = false;
        }

        private void materialExpansionPanel1_SaveClick(object sender, EventArgs e)
        {
            materialExpansionPanel1.Collapse = true;
            materialCheckedListBox2.Enabled = true;
            materialCheckedListBox1.Enabled = false;
        }

        private void materialExpansionPanel1_CancelClick(object sender, EventArgs e)
        {
            materialCheckedListBox1.Enabled = true;
            materialCheckedListBox2.Enabled = false;
            materialListView1.Visible = false;
        }
    }
}
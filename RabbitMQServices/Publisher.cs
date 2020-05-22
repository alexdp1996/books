

using Data.Repositories;
using Newtonsoft.Json;
using RabbitMQ;
using System.Configuration;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace RabbitMQServices
{
    partial class Publisher : ServiceBase
    {
        private Timer Timer { get; set; }

        public Publisher()
        {
            InitializeComponent();
            Timer = new Timer();
            Timer.Interval = 15000;
            Timer.Elapsed += OnTimer;
        }

        private void OnTimer(object sender, ElapsedEventArgs args)
        {
            using (var wrapper = new RabbitMQWrapper())
            {
                using (var authorRepo = new AuthorRepo())
                {
                    var authors = authorRepo.GetALL();
                    var json = JsonConvert.SerializeObject(authors);
                    var byteData = Encoding.UTF8.GetBytes(json);
                    wrapper.SendMessage(ConfigurationManager.AppSettings["RabbitMQExchange"], "Author", byteData);
                }

                using (var bookRepo = new BookRepo())
                {
                    var books = bookRepo.GetALL();
                    var json = JsonConvert.SerializeObject(books);
                    var byteData = Encoding.UTF8.GetBytes(json);
                    wrapper.SendMessage(ConfigurationManager.AppSettings["RabbitMQExchange"], "Book", byteData);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            Timer.Start();
        }

        protected override void OnStop()
        {
            Timer.Stop();
        }
    }
}

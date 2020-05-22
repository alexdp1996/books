using EntityModels;
using Newtonsoft.Json;
using RabbitMQ;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;

namespace RabbitMQServices
{
    partial class Consumer : ServiceBase
    {
        private RabbitMQWrapper RabbitMQ { get; set; }

        public Consumer()
        {
            InitializeComponent();
        }

        private string FormHTML(string header, string body)
        {
            var html = @"<html>
                            <body>
                                <table>
                                    <thead>
                                        {0}
                                    </thead>
                                    <tbody>
                                        {1}
                                    </tbody>
                                </table>
                            </body>
                         </html>";

            return string.Format(html, header, body);
        }

        private void SendEmail(string subject, string html)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"]);
            message.To.Add(ConfigurationManager.AppSettings["RecieverEmail"]);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = html;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderUserName"], ConfigurationManager.AppSettings["SenderPassword"]),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }

        protected override void OnStart(string[] args)
        {
            RabbitMQ = new RabbitMQWrapper();
            RabbitMQ.Consume("Books", (obj, ea) => {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var books = JsonConvert.DeserializeObject<IEnumerable<BookEM>>(json);

                var header = @"<tr>
                                 <th>ID</th><th>Name</th><th>Rate</th><th>Pages</th><th>Authors</th>
                               </tr>";

                var rowTemplate = @"<tr>
                                 <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>
                               </tr>";

                var strBuilder = new StringBuilder();

                foreach (var b in books)
                {
                    var authors = string.Join(", ", b.Authors.Select(a => a.Name + " " + a.Surname));
                    var row = string.Format(rowTemplate,b.Id, b.Name, b.Rate, b.Pages, authors);

                    strBuilder.Append(row);
                }

                string html = FormHTML(header, strBuilder.ToString());
                SendEmail("Books in BookCatalog", html);
            });

            RabbitMQ.Consume("Authors", (obj, ea) => {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var authors = JsonConvert.DeserializeObject<IEnumerable<AuthorEM>>(json);

                var header = @"<tr>
                                 <th>ID</th><th>Name</th><th>Surname</th><th>Books</th>
                               </tr>";

                var rowTemplate = @"<tr>
                                 <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>
                               </tr>";

                var strBuilder = new StringBuilder();

                foreach (var a in authors)
                {
                    var books = string.Join(", ", a.Books.Select(b => b.Name + "(pages: " + b.Pages + ")"));
                    var row = string.Format(rowTemplate, a.Id, a.Name, a.Surname, books);

                    strBuilder.Append(row);
                }

                string html = FormHTML(header, strBuilder.ToString());
                SendEmail("Authors in BookCatalog", html);
            });
        }

        protected override void OnStop()
        {
            RabbitMQ.Dispose();
        }
    }
}

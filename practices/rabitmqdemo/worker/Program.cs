using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace worker
{
    public class Program
    {
        public static async Task PostMessage(string postData)
        {
            var json = JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    //var result = await client.PostAsync("https://localhost:5001/api/values", content);

                    var result = await client.PostAsync("http://publisher_api:80/api/values", content);

                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine("Server returned: " + resultContent);
                }
            }
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Posting a message!");
            //PostMessage("test message").Wait();

        
            string[] testStrings = new string[] { "one", "two", "three", "four", "five" };

            Console.WriteLine("Sleeping to wait for Rabbit");
            Task.Delay(10000).Wait();
            Console.WriteLine("Posting messages to webApi");
            for (int i = 0; i < 5; i++)
            {
                PostMessage(testStrings[i]).Wait();
            }

            Task.Delay(1000).Wait();
            Console.WriteLine("Consuming Queue Now");

            ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                //var message = Encoding.UTF8.GetString(body);
                string message = body.ToString();
                //string message = "demo test message";
                Console.WriteLine(" [x] Received from Rabbit: {0}", message);
            };
            channel.BasicConsume(queue: "hello",
                                    autoAck: true,
                                    consumer: consumer);
        }

    }
}

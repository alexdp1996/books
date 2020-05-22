using System.ServiceProcess;

namespace RabbitMQServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Publisher(),
                new Consumer()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

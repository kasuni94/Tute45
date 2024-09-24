using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DataTier
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(DataServer));
            NetTcpBinding tcp = new NetTcpBinding();
            host.AddServiceEndpoint(typeof(DataServerInterface), tcp, "net.tcp://localhost:8100/DataService");

            host.Open();
            Console.WriteLine("Data Server is running... Press Enter to stop.");
            Console.ReadLine();

            host.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(BusinessServer));
            NetTcpBinding tcp = new NetTcpBinding();
            host.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://localhost:8200/BusinessService");

            host.Open();
            Console.WriteLine("Business Server is running... Press Enter to stop.");
            Console.ReadLine();

            host.Close();
        }
    }
}

using DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class BusinessServer : BusinessServerInterface
    {
        private readonly ChannelFactory<DataServerInterface> dataFactory;
        private readonly DataServerInterface dataServer;
        private uint logCount = 0;

        public BusinessServer()
        {
            dataFactory = new ChannelFactory<DataServerInterface>("net.tcp://localhost:8100/DataService");
            dataServer = dataFactory.CreateChannel();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Log(string message)
        {
            logCount++;
            Console.WriteLine($"Log {logCount}: {message}");
        }

        public List<Record> GetRecords()
        {
            Log("GetRecords called");
            return dataServer.GetRecords();
        }

        public Record GetRecordById(int id)
        {
            Log($"GetRecordById called for ID {id}");
            return dataServer.GetRecordById(id);
        }

        public Record SearchRecordByLastName(string lastName)
        {
            Log($"Search for {lastName} started");

            try
            {
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    throw new ArgumentException("Last name cannot be empty or null");
                }

                var result = dataServer.GetRecords().FirstOrDefault(r => r.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

                if (result == null)
                {
                    Log($"No record found for {lastName}");
                }
                else
                {
                    Log($"Record found: {result.FirstName} {result.LastName}");
                }

                return result; 
            }
            catch (TimeoutException ex)
            {
                Log($"Search for {lastName} timed out: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Log($"An error occurred while searching for {lastName}: {ex.Message}");
                throw; 
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace DataTier
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class DataServer : DataServerInterface
    {
        private List<Record> records;

        public DataServer()
        {

            records = new List<Record>();
            Random rand = new Random();
            for (int i = 0; i < 100000; i++)
            {
                records.Add(new Record
                {
                    Id = i,
                    FirstName = "FirstName" + i,
                    LastName = "LastName" + i,
                    Balance = rand.Next(1000, 10000)
                });
            }
        }

        public List<Record> GetRecords() => records;

        public Record GetRecordById(int id) => records.FirstOrDefault(r => r.Id == id);
    }
}

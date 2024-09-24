using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Xunit;

namespace DataTier
{
    [ServiceContract]
    public interface DataServerInterface
    {
        [OperationContract]
        List<Record> GetRecords();

        [OperationContract]
        Record GetRecordById(int id);
    }
}

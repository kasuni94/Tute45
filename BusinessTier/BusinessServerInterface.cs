using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DataTier;

namespace BusinessTier
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        List<Record> GetRecords();

        [OperationContract]
        Record GetRecordById(int id);

        [OperationContract]
        Record SearchRecordByLastName(string lastName);
    }
}

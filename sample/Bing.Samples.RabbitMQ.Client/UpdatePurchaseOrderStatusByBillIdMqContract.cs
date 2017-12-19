using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.RabbitMQ.Client
{
    public class UpdatePurchaseOrderStatusByBillIdMqContract
    {
        public int UpdatePurchaseOrderStatusType;
        public int RelationBillType;
        public int RelationBillId;
        public int UpdateStatus;
        public int ModifiedBy;
    }
}

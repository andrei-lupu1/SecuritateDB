using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Orders
{
    [Table("HISTORY_ORDERS")]
    public class HistoryOrder: DomainModelBase, IDomainModel
    {
        [ForeignKey("Order")]
        public int ORDER_ID { get; set; }

        public Order Order { get; set; }

        public string LOCATION { get; set; }

        public DateTime STATUS_DATE { get; set; }

        public int STATUS_ID { get; set; }  
    }
}

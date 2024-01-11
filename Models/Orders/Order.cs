using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Orders
{
    [Table("ORDERS")]
    public class Order: DomainModelBase, IDomainModel
    {
        [ForeignKey("Customer")]
        public int CUSTOMER_ID { get; set; }

        public Person.Person Customer { get; set; }

        [ForeignKey("Courier")]
        public int COURIER_ID { get; set; }

        public Person.Person Courier { get; set; }

        public string DESCRIPTION { get; set; }

        public int PIN_CODE { get; set; }
        
        public int PAYMENT_METHOD_ID { get; set; }

        public int AMMOUNT { get; set; }

        public string AWB { get; set; }

        public int? SENDER_ID { get; set; }

        public IEnumerable<HistoryOrder> HistoryOrders { get; set; }
    }
}

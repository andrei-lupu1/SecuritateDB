using Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Orders
{
    [Table("HISTORY_ORDERS")]
    public class HistoryOrder: DomainModelBase, IDomainModel
    {
        [ForeignKey("order")]
        public int ORDER_ID { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
        public string LOCATION { get; set; }

        public DateTime STATUS_DATE { get; set; }

        public int STATUS_ID { get; set; }  
    }
}

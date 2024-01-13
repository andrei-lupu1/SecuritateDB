using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformationObjects.Payloads
{
    public class OrderOutput
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public int PinCode { get; set; }

        public int Ammount { get; set; }

        public string PaymentMethod { get; set; }

        public string Awb { get; set; }

        public HistoryOrderOutput[] HistoryOrders { get; set; }


    }
}

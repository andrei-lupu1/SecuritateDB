using Models.Person;

namespace DataTransformationObjects.Payloads
{
    public class OrderPayload
    {
        public string Description { get; set; }

        public int Ammount { get; set; }

        public int PaymentMethodId { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhone { get; set; }

        public string RecipientEmail { get; set; }

        public string Address { get; set; }

        public int ZipCode { get; set; }

        public int CityId { get; set; }
    }
}

using Models.Person;

namespace DataTransformationObjects.Payloads
{
    public class OrderPayload
    {
        public string Description { get; set; }

        public int Ammount { get; set; }

        public int PaymentMethodId { get; set; }

        public Address Address { get; set; }
    }
}

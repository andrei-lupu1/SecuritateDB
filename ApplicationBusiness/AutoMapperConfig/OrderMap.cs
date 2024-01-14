using AutoMapper;
using DataTransformationObjects.Orders;
using Models.Enums;
using Models.Orders;

namespace ApplicationBusiness.AutoMapperConfig
{
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<Order, OrderOutput>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address.ADDRESS))
                .ForMember(dest => dest.Ammount, opt => opt.MapFrom(src => src.AMMOUNT))
                .ForMember(dest => dest.Awb, opt => opt.MapFrom(src => src.AWB))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Customer.Address.City.NUME))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.NUME))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.EMAIL))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => Enum.GetName(typeof(PaymentMethodsEnum), src.PAYMENT_METHOD_ID)))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Customer.TELEFON))
                .ForMember(dest => dest.PinCode, opt => opt.MapFrom(src => src.PIN_CODE))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Customer.Address.ZIP_CODE))
                .ForMember(dest => dest.HistoryOrders, opt => opt.MapFrom(src => src.HistoryOrders));

            CreateMap<HistoryOrder,HistoryOrderOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(StatusesEnum), src.STATUS_ID)))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LOCATION))
                .ForMember(dest => dest.StatusDate, opt => opt.MapFrom(src => src.STATUS_DATE.ToString("dd.MM.yyyy")));
        }
    }
}

using AutoMapper;

namespace CourierCostCalculator.API.Mappings;

public class ParcelMapping : Profile
{
    public ParcelMapping()
    {
        CreateMap<Parcel, Lib.Models.Parcel>();
    }
}
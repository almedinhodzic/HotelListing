using HotelListing.API.Data;

namespace HotelListing.API.Business.Contracts
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country>? GetDetails(int? id);
    }
}

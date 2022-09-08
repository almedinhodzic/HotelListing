using HotelListing.API.Business.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Business.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly HotelListingDbContext context;

        public CountryRepository(HotelListingDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Country>? GetDetails(int? id)
        {
            return await context.Countries
                .Include(q => q.Hotels)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

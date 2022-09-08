using HotelListing.API.Business.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Business.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly HotelListingDbContext context;

        public HotelRepository(HotelListingDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

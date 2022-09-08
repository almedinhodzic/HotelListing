using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Models.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }

        public List<GetHotelDto>? Hotels { get; set; }
    }
}

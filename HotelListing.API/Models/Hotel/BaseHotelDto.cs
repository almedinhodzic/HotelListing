using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Hotel
{
    public abstract class BaseHotelDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Range(0, 5.00)]
        public double? Rating { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}

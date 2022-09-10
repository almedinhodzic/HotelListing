using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Business.Contracts;
using AutoMapper;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;

        public HotelsController(IHotelRepository hotelRepository,
            IMapper mapper)
        {
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await hotelRepository.GetAllAsync();
            var records = mapper.Map<List<Hotel>, List<HotelDto>>(hotels);
            return Ok(records);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var record = mapper.Map<Hotel, HotelDto>(hotel);
            return Ok(record);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotelDto)
        {
            if(id != hotelDto.Id)
            {
                return BadRequest();
            }

            var entity = await hotelRepository.GetAsync(id);

            if(entity == null)
            {
                return NotFound();
            }

            mapper.Map(hotelDto, entity);

            try
            {

                await hotelRepository.UpdateAsync(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotel)
        {
            var record = mapper.Map<CreateHotelDto, Hotel>(hotel);
            await hotelRepository.AddAsync(record);

            return CreatedAtAction("GetHotel", new { id = record.Id }, record);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await hotelRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await hotelRepository.DeleteAsync(id);

            return NoContent();
        }
         
        private async Task<bool> HotelExists(int id)
        {
            return await hotelRepository.Exists(id);
        }
    }
}

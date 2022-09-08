using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Business.Contracts;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepository countryRepository,
            IMapper mapper)
        {
            this.countryRepository = countryRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        /// <summary>
        /// Get All Countries
        /// </summary>
        /// <returns>All Countries</returns>
        [HttpGet(Name = "GetCountries")]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await countryRepository.GetAllAsync();
            var countriesDto = _mapper.Map<List<Country>, List< GetCountryDto>> (countries);
            return Ok(countriesDto);
        }

        // GET: api/Countries/5
        [HttpGet("{id:int}", Name = "GetCountry")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await countryRepository.GetDetails(id);

            if (country == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<Country, CountryDto>(country);

            return Ok(record);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}", Name = "PutCountry")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest();
            }


            var country = await countryRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDto, country);

            await countryRepository.UpdateAsync(country);

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "PostCountry")]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            var country = _mapper.Map<Country>(createCountryDto);
            await countryRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id:int}", Name = "DeleteCountry")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await countryRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await countryRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await countryRepository.Exists(id);
        }
    }
}

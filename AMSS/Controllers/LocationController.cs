using AMSS.Enums;
using AMSS.Models;
using AMSS.Models.Dto.Farm;
using AMSS.Models.Dto.Location;
using AMSS.Repositories;
using AMSS.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public LocationController(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<APIResponse>> GetAllLocations()
        {
            try
            {
                List<Location> lstLocations = await _locationRepository.GetAllAsync();
                List<LocationDto> lstLocationDtos = _mapper.Map<List<LocationDto>>(lstLocations);
                if (lstLocations == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Something wrong when get all Locations");
                    return NotFound(_response);
                }
                _response.Result = lstLocationDtos;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("getLocationById/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetLocationById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get Location by id");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Location location = await _locationRepository.GetAsync(u => u.Id == id);
                if (location == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get Location by id");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                LocationDto locationDto = _mapper.Map<LocationDto>(location);
                _response.Result = locationDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public async Task<ActionResult<APIResponse>> CreateFarm(CreateLocationDto createLocationDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newLocation = _mapper.Map<Location>(createLocationDto);
                    newLocation.CreatedAt = DateTime.Now;
                    newLocation.UpdatedAt = DateTime.Now;

                    await _locationRepository.CreateAsync(newLocation);
                    await _locationRepository.SaveAsync();
                    _response.Result = newLocation;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("getLocationById", new { id = newLocation.Id }, _response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when creating new Location");
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public async Task<ActionResult<APIResponse>> UpdateMenuItem(int id, [FromForm] LocationDto updateLocationDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updateLocationDto == null || id != updateLocationDto.Id)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("This Location does not exist!");
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest();
                    }

                    Location locationFromDb = await _locationRepository.GetAsync(u => u.Id == id, false);

                    if (locationFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("Not found this Location");
                        return NotFound(_response);
                    }
                    locationFromDb = _mapper.Map<Location>(updateLocationDto);
                    locationFromDb.UpdatedAt = DateTime.Now;

                    await _locationRepository.Update(locationFromDb);
                    await _locationRepository.SaveAsync();
                    _response.Result = locationFromDb;
                    _response.SuccessMessage = "Location updated successfully 🌿";
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when updating location");
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public async Task<ActionResult<APIResponse>> DeleteMenuItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("This Location does not exist!");
                    return BadRequest(_response);
                }

                Location locationFromDb = await _locationRepository.GetAsync(u => u.Id == id);
                if (locationFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Not found this Location");
                    return NotFound(_response);
                }

                await _locationRepository.RemoveAsync(locationFromDb);
                await _locationRepository.SaveAsync();
                _response.SuccessMessage = "Location deleted successfully !";
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}

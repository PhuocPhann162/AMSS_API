using AMSS.Enums;
using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Models.Dto.Farm;
using AMSS.Repositories;
using AMSS.Repositories.IRepository;
using AMSS.Utility;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/farm")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly IFarmRepository _farmRepository;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public FarmController(IFarmRepository farmRepository, IMapper mapper)
        {
            _farmRepository = farmRepository;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<APIResponse>> GetAllFarms()
        {
            try
            {
                List<Farm> lstFarms = await _farmRepository.GetAllAsync(includeProperties: "Location");
                
                _response.Result = _mapper.Map<List<FarmDto>>(lstFarms);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("getFarmById/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetFarmById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get Farm by id");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Farm farm = await _farmRepository.GetAsync(u => u.Id == id);
                if (farm == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get Farm by id");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                FarmDto farmDto = _mapper.Map<FarmDto>(farm);
                _response.Result = farmDto;
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
        public async Task<ActionResult<APIResponse>> CreateFarm([FromForm]CreateFarmDto createFarmDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newFarm = _mapper.Map<Farm>(createFarmDto);
                    newFarm.CreatedAt = DateTime.Now;
                    newFarm.UpdatedAt = DateTime.Now;

                    await _farmRepository.CreateAsync(newFarm);
                    await _farmRepository.SaveAsync();
                    _response.Result = newFarm;
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.SuccessMessage = "Farm created successfully";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when creating new Farm");
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
        public async Task<ActionResult<APIResponse>> UpdateFarm(int id, [FromForm] FarmDto updateFarmDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updateFarmDto == null || id != updateFarmDto.Id)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("This Farm does not exist!");
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest();
                    }

                    Farm farmFromDb = await _farmRepository.GetAsync(u => u.Id == id, false);

                    if (farmFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("Not found this Farm");
                        return NotFound(_response);
                    }
                    farmFromDb = _mapper.Map<Farm>(updateFarmDto);
                    farmFromDb.UpdatedAt = DateTime.Now;

                    await _farmRepository.Update(farmFromDb);
                    await _farmRepository.SaveAsync();
                    _response.Result = farmFromDb;
                    _response.SuccessMessage = "Farm updated successfully 🌿";
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when updating farm");
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
        public async Task<ActionResult<APIResponse>> DeleteFarm(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("This Farm does not exist!");
                    return BadRequest(_response);
                }

                Farm farmFromDb = await _farmRepository.GetAsync(u => u.Id == id);
                if (farmFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Not found this Farm");
                    return NotFound(_response);
                }

                await _farmRepository.RemoveAsync(farmFromDb);
                await _farmRepository.SaveAsync();
                _response.SuccessMessage = "Farm deleted successfully !";
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

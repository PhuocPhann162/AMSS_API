using AMSS.Enums;
using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Models.Dto.Farm;
using AMSS.Models.Dto.Polygon;
using AMSS.Models.Polygon;
using AMSS.Repositories;
using AMSS.Repositories.IRepository;
using AMSS.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/polygon")]
    [ApiController]
    public class PolygonController : ControllerBase
    {
        private readonly IPolygonAppRepository _polygonRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public PolygonController(IPositionRepository positionRepository, IPolygonAppRepository polygonRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _polygonRepository = polygonRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<APIResponse>> GetAllPolygons()
        {
            try
            {
                IEnumerable<PolygonApp> lstPolygon = await _polygonRepository.GetAllAsync();
                var lstPolygonDto = _mapper.Map<PolygonDto>(_polygonRepository);
                _response.Result = lstPolygonDto;
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

        [HttpGet("getPolygonByFarmId/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetPolygonByFarmId(int farmId)
        {
            try
            {
                if (farmId == null || farmId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Something wrong when get polygon");
                    return NotFound(_response);
                }
                PolygonApp polygon = await _polygonRepository.GetAsync(u => u.FarmId == farmId);
                if (polygon == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Something wrong when get polygon");
                    return NotFound(_response);
                }
                var polygonDto = _mapper.Map<PolygonDto>(polygon);
                var lstPositions = await _positionRepository.GetAllAsync(u => u.PolygonAppId == polygon.Id);
                polygonDto.Positions = lstPositions;

                _response.Result = polygonDto;
                _response.SuccessMessage = "Get polygon successfully";
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
        public async Task<ActionResult<APIResponse>> CreatePolygon(CreatePolygonDto createPolygonDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPolygon = _mapper.Map<PolygonApp>(createPolygonDto);
                    

                    await _polygonRepository.CreateAsync(newPolygon);
                    await _polygonRepository.SaveAsync();

                    foreach (var pos in createPolygonDto.Positions)
                    {
                        pos.PolygonAppId = newPolygon.Id;
                        pos.PolygonApp = newPolygon;
                        _positionRepository.CreateAsync(pos);
                    }

                    _response.Result = newPolygon;
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.SuccessMessage = "Polygon created successfully";
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
    }
}

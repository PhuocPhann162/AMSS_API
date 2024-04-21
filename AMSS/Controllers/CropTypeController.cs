using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Models.Dto.CropType;
using AMSS.Repositories;
using AMSS.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/cropType")]
    [ApiController]
    public class CropTypeController : ControllerBase
    {
        private readonly ICropTypeRepository _cropTypeRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CropTypeController(ICropTypeRepository cropTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cropTypeRepository = cropTypeRepository;
            _response = new APIResponse();
        }

        [HttpGet("getAllCropTypes")]
        public async Task<ActionResult<APIResponse>> GetAllCropTypes()
        {
            try
            {
                IEnumerable<CropType> lstCropTypes = await _cropTypeRepository.GetAllAsync(includeProperties: "Crops");
                var lstCropTypeDtos = _mapper.Map<IEnumerable<CropTypeDto>>(lstCropTypes);
                if (lstCropTypes == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Something wrong when get all crop types");
                    return NotFound(_response);
                }
                _response.Result = lstCropTypeDtos;
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
    }
}

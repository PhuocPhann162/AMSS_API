using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Models.Dto.CropType;
using AMSS.Models.Dto.Field;
using AMSS.Repositories;
using AMSS.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AMSS.Controllers
{
    [Route("api/cropType")]
    [ApiController]
    public class CropTypeController : ControllerBase
    {
        private readonly ICropTypeRepository _cropTypeRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CropTypeController(ICropTypeRepository cropTypeRepository, IFieldRepository fieldRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cropTypeRepository = cropTypeRepository;
            _fieldRepository = fieldRepository;
            _response = new APIResponse();
        }

        [HttpGet("getAllCropTypes")]
        public async Task<ActionResult<APIResponse>> GetAllCropTypes(string? searchString, int? pageNumber, int? pageSize)
        {
            try
            {
                IEnumerable<CropType> lstCropTypes = await _cropTypeRepository.GetAllAsync(includeProperties: "Crops");
                var lstCropTypeDtos = _mapper.Map<IEnumerable<CropTypeDto>>(lstCropTypes);
                foreach (var ct in lstCropTypeDtos)
                {
                    foreach (var cr in ct.Crops)
                    {
                        cr.Field =_mapper.Map<FieldDto>(_fieldRepository.GetAsync(u => u.Id == cr.FieldId).GetAwaiter().GetResult());
                    }
                }
                if (!string.IsNullOrEmpty(searchString))
                {
                    lstCropTypeDtos = lstCropTypeDtos.Where(u => u.Name.ToLower().Contains(searchString.ToLower()) || u.Code.ToLower().Contains(searchString.ToLower()));
                }

                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    Pagination pagination = new()
                    {
                        CurrentPage = pageNumber,
                        PageSize = pageSize,
                        TotalRecords = lstCropTypeDtos.Count()
                    };
                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                    _response.Result = lstCropTypeDtos.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
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

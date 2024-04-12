using AMSS.Enums;
using AMSS.Models;
using AMSS.Models.Dto.Crop;
using AMSS.Repositories.IRepository;
using AMSS.Services.IService;
using AMSS.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/crop")]
    [ApiController]
    public class CropController : ControllerBase
    {
        private readonly ICropRepository _cropRepository;
        private readonly IBlobService _blobService;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public CropController(ICropRepository cropRepository, IBlobService blobService, IMapper mapper)
        {
            _cropRepository = cropRepository;
            _blobService = blobService;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetCrops()
        {
            try
            {
                List<Crop> lstCrops = await _cropRepository.GetAllAsync();
                List<CropDto> lstCropDtos = _mapper.Map<List<CropDto>>(lstCrops);
                if (lstCrops == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Something wrong when get all crops");
                    return NotFound(_response);
                }
                _response.Result = lstCropDtos;
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

        [HttpGet("getCropById/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetCropById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get crop by id");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Crop crop = await _cropRepository.GetAsync(u => u.Id == id);
                CropDto cropDto = _mapper.Map<CropDto>(crop);
                if (crop == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get crop by id");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = cropDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
                _response.IsSuccess = false;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public async Task<ActionResult<APIResponse>> CreateMenuItem([FromForm] CreateCropDto createCropDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createCropDto.File == null || createCropDto.File.Length == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages.Add("File is required");
                        return BadRequest(_response);
                    }

                    var newCrop = _mapper.Map<Crop>(createCropDto);
                    newCrop.CreatedAt = DateTime.Now;
                    newCrop.UpdatedAt = DateTime.Now;

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(createCropDto.File.FileName)}";
                    newCrop.Icon = await _blobService.UploadBlob(fileName, SD.SD_Storage_Container, createCropDto.File);

                    await _cropRepository.CreateAsync(newCrop);
                    await _cropRepository.SaveAsync();
                    _response.Result = newCrop;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("getCropById", new { id = newCrop.Id }, _response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when creating new crop");
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
        public async Task<ActionResult<APIResponse>> UpdateMenuItem(int id, [FromForm] UpdateCropDto updateCropDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updateCropDto == null || id != updateCropDto.Id)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("This crop does not exist!");
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest();
                    }

                    Crop cropFromDb = await _cropRepository.GetAsync(u => u.Id == id, false);

                    if (cropFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("Not found this crop");
                        return NotFound(_response);
                    }
                    cropFromDb = _mapper.Map<Crop>(updateCropDto);
                    cropFromDb.UpdatedAt = DateTime.Now;

                    if (updateCropDto.File != null && updateCropDto.File.Length > 0)
                    {
                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(updateCropDto.File.FileName)}";
                        await _blobService.DeleteBlob(cropFromDb.Icon.Split('/').Last(), SD.SD_Storage_Container);
                        cropFromDb.Icon = await _blobService.UploadBlob(fileName, SD.SD_Storage_Container, updateCropDto.File);
                    }
                    await _cropRepository.Update(cropFromDb);
                    await _cropRepository.SaveAsync();
                    _response.Result = cropFromDb;
                    _response.SuccessMessage = "Crop updated successfully 🌿";
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when updating crop");
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
                    _response.ErrorMessages.Add("This crop does not exist!");
                    return BadRequest(_response);
                }

                Crop cropFromDb = await _cropRepository.GetAsync(u => u.Id == id);
                if (cropFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Not found this menu item");
                    return NotFound(_response);
                }

                await _blobService.DeleteBlob(cropFromDb.Icon.Split('/').Last(), SD.SD_Storage_Container);
                int milliseconds = 2000;
                Thread.Sleep(milliseconds);

                await _cropRepository.RemoveAsync(cropFromDb);
                await _cropRepository.SaveAsync();
                _response.SuccessMessage = "Crop deleted successfully !";
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

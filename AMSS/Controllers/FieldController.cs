using AMSS.Enums;
using AMSS.Models.Dto.Field;
using AMSS.Models;
using AMSS.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMSS.Controllers
{
    [Route("api/field")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public FieldController(IFieldRepository fieldRepository, IMapper mapper)
        {
            _fieldRepository = fieldRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<APIResponse>> GetAllFields()
        {
            try
            {
                List<Field> lstFields = await _fieldRepository.GetAllAsync(includeProperties: "Location");
                _response.Result = _mapper.Map<List<FieldDto>>(lstFields);
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

        [HttpGet("getFieldById/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetFieldById(int id)
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
                Field Field = await _fieldRepository.GetAsync(u => u.Id == id);
                if (Field == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get crop by id");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                FieldDto FieldDto = _mapper.Map<FieldDto>(Field);
                _response.Result = FieldDto;
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
        public async Task<ActionResult<APIResponse>> CreateField([FromForm]CreateFieldDto createFieldDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newField = _mapper.Map<Field>(createFieldDto);
                    newField.CreatedAt = DateTime.Now;
                    newField.UpdatedAt = DateTime.Now;

                    await _fieldRepository.CreateAsync(newField);
                    await _fieldRepository.SaveAsync();
                    _response.Result = newField;
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.SuccessMessage = "Field created successfully";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when creating new Field");
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
        public async Task<ActionResult<APIResponse>> UpdateField(int id, [FromForm] FieldDto updateFieldDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updateFieldDto == null || id != updateFieldDto.Id)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("This Field does not exist!");
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest();
                    }

                    Field fieldFromDb = await _fieldRepository.GetAsync(u => u.Id == id, false);

                    if (fieldFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("Not found this Field");
                        return NotFound(_response);
                    }
                    fieldFromDb = _mapper.Map<Field>(updateFieldDto);
                    fieldFromDb.UpdatedAt = DateTime.Now;

                    await _fieldRepository.Update(fieldFromDb);
                    await _fieldRepository.SaveAsync();
                    _response.Result = fieldFromDb;
                    _response.SuccessMessage = "Field updated successfully 🌿";
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when updating Field");
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
        public async Task<ActionResult<APIResponse>> DeleteField(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("This Field does not exist!");
                    return BadRequest(_response);
                }

                Field FieldFromDb = await _fieldRepository.GetAsync(u => u.Id == id);
                if (FieldFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Not found this Field");
                    return NotFound(_response);
                }

                await _fieldRepository.RemoveAsync(FieldFromDb);
                await _fieldRepository.SaveAsync();
                _response.SuccessMessage = "Field deleted successfully !";
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

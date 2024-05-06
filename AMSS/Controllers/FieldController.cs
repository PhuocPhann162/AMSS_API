﻿using AMSS.Enums;
using AMSS.Models.Dto.Field;
using AMSS.Models;
using AMSS.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AMSS.Repositories;
using System.Text.Json;
using AMSS.Utility;
using Azure.Core.GeoJson;
using AMSS.Models.Polygon;

namespace AMSS.Controllers
{
    [Route("api/field")]
    [ApiController]
    [Authorize]
    public class FieldController : ControllerBase
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPolygonAppRepository _polygonAppRepository;
        protected APIResponse _response;
        private readonly IMapper _mapper;
        public FieldController(IFieldRepository fieldRepository, IPositionRepository positionRepository, IPolygonAppRepository polygonAppRepository, ILocationRepository locationRepository, IMapper mapper)
        {
            _fieldRepository = fieldRepository;
            _positionRepository = positionRepository;
            _locationRepository = locationRepository;
            _polygonAppRepository = polygonAppRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("getAll")]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public async Task<ActionResult<APIResponse>> GetAllFields(string? searchString, string? status, int? pageNumber, int? pageSize)
        {
            try
            {
                IEnumerable<Field> lstFields = await _fieldRepository.GetAllAsync(includeProperties: "Location,PolygonApp,Farm");
                var lstFieldsDto = _mapper.Map<IEnumerable<FieldDto>>(lstFields);

                foreach (var f in lstFieldsDto)
                {
                    if (f.PolygonApp != null)
                    {
                        f.PolygonApp.Positions = await _positionRepository.GetAllAsync(u => u.PolygonAppId == f.PolygonApp.Id);
                    }
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    lstFieldsDto = lstFieldsDto.Where(u => u.Name.ToLower().Contains(searchString.ToLower()) || u.Farm.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    lstFieldsDto = lstFieldsDto.Where(u => u.Status.ToLower() == status.ToLower());
                }

                if (pageNumber.HasValue && pageSize.HasValue)
                {
                    Pagination pagination = new()
                    {
                        CurrentPage = pageNumber,
                        PageSize = pageSize,
                        TotalRecords = lstFieldsDto.Count(),
                    };
                    Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                    _response.Result = lstFieldsDto.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                _response.Result = lstFieldsDto;
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
        [Authorize(Roles = nameof(Role.ADMIN))]
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
                Field fieldFromDb = await _fieldRepository.GetAsync(u => u.Id == id, includeProperties: "Location,PolygonApp,Farm");
                if (fieldFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Oops ! Something wrong when get crop by id");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                fieldFromDb.PolygonApp.Positions = await _positionRepository.GetAllAsync(u => u.PolygonAppId == fieldFromDb.PolygonApp.Id);
                FieldDto FieldDto = _mapper.Map<FieldDto>(fieldFromDb);
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
        public async Task<ActionResult<APIResponse>> CreateField([FromForm] CreateFieldDto createFieldDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newField = _mapper.Map<Field>(createFieldDto);

                    newField.Status = String.IsNullOrEmpty(newField.Status) ? SD.Status_Idle : newField.Status;

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
        public async Task<ActionResult<APIResponse>> UpdateField(int id, [FromBody] UpdateFieldDto updateFieldDto)
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

                    Field fieldFromDb = await _fieldRepository.GetAsync(u => u.Id == id, includeProperties: "Location,PolygonApp");
                    List<Position> lstPositionsFromDb = await _positionRepository.GetAllAsync(u => u.PolygonAppId == fieldFromDb.PolygonAppId);
                    if (fieldFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("Not found this Field");
                        return NotFound(_response);
                    }


                    // Update Position for Polygon

                    if (updateFieldDto.Positions != null)
                    {
                        int noPositions = lstPositionsFromDb.Count();
                        var updatePositions = updateFieldDto.Positions.ToList(); 

                        for (int i = 0; i < noPositions; i++) 
                        {
                            var posFromDb = lstPositionsFromDb[i];
                            posFromDb.Lat = updatePositions[i].Lat;
                            posFromDb.lng = updatePositions[i].lng;
                            await _positionRepository.Update(posFromDb);
                        }

                        if (noPositions < updateFieldDto.Positions.Count())
                        {
                            for (int i = noPositions; i < updateFieldDto.Positions.Count(); i++)
                            {
                                updatePositions[i].PolygonAppId = fieldFromDb.PolygonAppId;
                                updatePositions[i].PolygonApp = fieldFromDb.PolygonApp;
                                await _positionRepository.CreateAsync(updatePositions[i]);
                            }
                        }
                        await _positionRepository.SaveAsync();
                    }

                    if(updateFieldDto.Location != null)
                    {
                        // Update Field Location
                        var locationFromDb = _locationRepository.GetAsync(u => u.Id == fieldFromDb.LocationId).GetAwaiter().GetResult();
                        locationFromDb = _mapper.Map<Location>(updateFieldDto.Location);
                        await _locationRepository.Update(fieldFromDb.Location);
                        await _locationRepository.SaveAsync();
                    }

                    // Update Field Props
                    if (!string.IsNullOrEmpty(updateFieldDto.Name))
                    {
                        fieldFromDb.Name = updateFieldDto.Name;
                    }

                    if (!string.IsNullOrEmpty(updateFieldDto.Status))
                    {
                        fieldFromDb.Status = updateFieldDto.Status;
                    }

                    if (updateFieldDto.Area.HasValue)
                    {
                        fieldFromDb.Area = updateFieldDto.Area ?? fieldFromDb.Area;
                    }

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

                Field fieldFromDb = await _fieldRepository.GetAsync(u => u.Id == id, includeProperties: "Location,PolygonApp");
                List<Position> lstPositionsFromDb = await _positionRepository.GetAllAsync(u => u.PolygonAppId == fieldFromDb.PolygonAppId);
                // Delete Location 
                await _locationRepository.RemoveAsync(fieldFromDb.Location);
                await _locationRepository.SaveAsync();
                // Delete Positions
                foreach(Position pos in lstPositionsFromDb)
                {
                    await _positionRepository.RemoveAsync(pos);
                }
                await _positionRepository.SaveAsync();
                // Delete PolygonApp 
                await _polygonAppRepository.RemoveAsync(fieldFromDb.PolygonApp);
                await _polygonAppRepository.SaveAsync();


                if (fieldFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Not found this Field");
                    return NotFound(_response);
                }

                await _fieldRepository.RemoveAsync(fieldFromDb);
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

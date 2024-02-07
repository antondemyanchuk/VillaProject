using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaProject_API.Models;
using VillaProject_API.Models.DTO;
using VillaProject_API.Repository.IRepository;

namespace VillaProject_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/VillaProject")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaProjectController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<VillaProjectController> _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaProjectController(ILogger<VillaProjectController> logger, IVillaRepository villaRepository, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = villaRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogInformation("Getting all villas");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // If !<VillaDTO> in ActionResult
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(VillaDTO))]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Get Villa Error with Id" + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(v => v.Id == id);

                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogInformation("Getting villa by Id");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError("Exception Getting Villa by Id");
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createVillaDTO)
        {
            // Checking requirements if ![ApiController]
            //if(!ModelState.IsValid)
            //    return BadRequest(ModelState);

            try
            {
                //Checking and validation data
                if (await _dbVilla.GetAsync(v => v.Name.ToLower() == createVillaDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa already exist!");
                    _response.IsSuccess = false;
                    _logger.LogInformation("Villa already exists with the specified number");
                    return BadRequest(ModelState);
                }

                if (createVillaDTO == null)
                {
                    return BadRequest(createVillaDTO);
                }

                //Manual mode creating

                //Villa model = new() { 
                //    Name = villaDTO.Name,
                //    Details = villaDTO.Details,
                //    Amenity = villaDTO.Amenity,
                //    ImageUrl = villaDTO.ImageUrl,
                //    Occupancy = villaDTO.Occupancy,
                //    Rate = villaDTO.Rate,
                //    Sqft = villaDTO.Sqft,
                //};

                //Automapper model creating
                Villa villa = _mapper.Map<Villa>(createVillaDTO);

                await _dbVilla.CreateAsync(villa);

                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.Created;
                _logger.LogInformation("Added new villa");
                //return Ok(villaDTO);
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogInformation("Not Valid input data");
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _logger.LogInformation("The Villa does not found");
                    return NotFound(_response);
                }
                await _dbVilla.RemoveAsync(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _logger.LogInformation("Villa deleted");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateVillaDTO)
        {
            if (updateVillaDTO == null || id != updateVillaDTO.Id)
            {
                return BadRequest();
            }
            try
            {

                //Villa model = new() {
                //    Id = updateVillaDTO.Id,
                //    Name = updateVillaDTO.Name,
                //    Details = updateVillaDTO.Details,
                //    Amenity = updateVillaDTO.Amenity,
                //    ImageUrl = updateVillaDTO.ImageUrl,
                //    Occupancy = updateVillaDTO.Occupancy,
                //    Rate = updateVillaDTO.Rate,
                //    Sqft = updateVillaDTO.Sqft,
                //};

                Villa model = _mapper.Map<Villa>(updateVillaDTO);

                await _dbVilla.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
                var villa = await _dbVilla.GetAsync(v => v.Id == id, tracked: false);

                //VillaUpdateDTO villaDTO = new()
                //{
                //    Id = villa.Id,
                //    Name = villa.Name,
                //    Details = villa.Details,
                //    Amenity = villa.Amenity,
                //    ImageUrl = villa.ImageUrl,
                //    Occupancy = villa.Occupancy,
                //    Rate = villa.Rate,
                //};
                VillaUpdateDTO villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(villa);

                if (villa == null)
                {
                    return NotFound();
                }
                patchDTO.ApplyTo(villaUpdateDTO, ModelState);

                //Villa model = new()
                //{
                //    Id = villaUpdateDTO.Id,
                //    Name = villaUpdateDTO.Name,
                //    Details = villaUpdateDTO.Details,
                //    Amenity = villaUpdateDTO.Amenity,
                //    ImageUrl = villaUpdateDTO.ImageUrl,
                //    Occupancy = villaUpdateDTO.Occupancy,
                //    Rate = villaUpdateDTO.Rate,
                //};
                Villa model = _mapper.Map<Villa>(villaUpdateDTO);

                await _dbVilla.UpdateAsync(model);

                if (ModelState.IsValid) { return BadRequest(); }

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}

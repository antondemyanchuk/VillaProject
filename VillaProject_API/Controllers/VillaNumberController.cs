using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaProject_API.Models;
using VillaProject_API.Models.DTO;
using VillaProject_API.Repository.IRepository;

namespace VillaProject_API.Controllers
{
    [Route("api/VillaNumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IVillaNumberRepository _numberRepository;
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;

        public VillaNumberController(ILogger<VillaNumberController> logger, IVillaNumberRepository numberRepository, IVillaRepository villaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _villaRepository = villaRepository; 
            _numberRepository = numberRepository;
            this._response = new();
        }

        [HttpGet(Name = "GetAllNumbers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbers = await _numberRepository.GetAllAsync(includeProperties:"Villa");
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumbers);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogInformation("Getting All Numbers");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
                _logger.LogError("Exception Getting All Numbers");
            }
            return _response;
        }

        [HttpGet("{villaNo:int}",Name = "GetNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumber(int villaNo)
        {
            try
            {
                if (villaNo == 0)
                {
                    _logger.LogError("Get Villa Error with Id" + villaNo);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var villaNumber = await _numberRepository.GetAsync(n => n.VillaNo == villaNo);

                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _logger.LogInformation("Villa with the specified parameters was not found ");
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                _logger.LogInformation("Getting Number by No");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = [ex.ToString()];
                _logger.LogError("Exception Getting Number by No");
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumber([FromBody] VillaNumberCreateDTO createVillaNumberDTO)
        {
            try
            {
                if (await _numberRepository.GetAsync(v => v.VillaNo == createVillaNumberDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("NumberExistError", "Number already exist!");
                    _response.IsSuccess = false;
                    _logger.LogInformation("Number already exists with the specified number");
                    return BadRequest(_response);
                }
                if (await _villaRepository.GetAsync(v => v.Id == createVillaNumberDTO.VillaId) == null)
                {
                    //ModelState.AddModelError("VillaExistError", "Villa with provided Id doesn't exist!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorsMessages = ["Villa with provided Id doesn't exist!"];
                    _logger.LogInformation("Villa with provided Id doesn't exist!");
                    return BadRequest(_response);
                }


                if (createVillaNumberDTO == null)
                {
                    return BadRequest(createVillaNumberDTO);
                }

                var villaNumber = _mapper.Map<VillaNumber>(createVillaNumberDTO);

                await _numberRepository.CreateAsync(villaNumber);

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                _logger.LogInformation("Added new Number");

                return CreatedAtRoute("GetNumber", new { villaNo = villaNumber.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
                _logger.LogError("Exception Creating new Number");
            }
            return _response;
        }

        [HttpDelete("{villaNo:int}", Name = "DeleteNumber")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteNumber(int villaNo)
        {
            try
            {
                if (villaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogInformation("Not Valid input data");
                    return BadRequest(_response);
                }
                var villaNumber = await _numberRepository.GetAsync(v => v.VillaNo == villaNo);
                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _logger.LogInformation("The Number does not found");
                    return NotFound(_response);
                }
                await _numberRepository.RemoveAsync(villaNumber);

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _logger.LogInformation("Number deleted");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{villaNo:int}", Name = "UpdateNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateNumber(int villaNo, [FromBody] VillaNumberUpdateDTO updateVillaNumberDTO)
        {
            try
            {
                if (updateVillaNumberDTO == null || villaNo != updateVillaNumberDTO.VillaNo)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _logger.LogInformation("Not Valid input data");
                    return BadRequest(_response);
                }
                if (await _villaRepository.GetAsync(v => v.Id == updateVillaNumberDTO.VillaId) == null)
                {
                    ModelState.AddModelError("VillaExistError", "Villa with provided Id doesn't exist!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _logger.LogInformation("Villa with provided Id doesn't exist!");
                    return BadRequest(_response);
                }

                var model = _mapper.Map<VillaNumber>(updateVillaNumberDTO);

                await _numberRepository.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                _logger.LogInformation("Number updated");
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}


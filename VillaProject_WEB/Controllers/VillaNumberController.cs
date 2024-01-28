using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VillaProject_WEB.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Models.VM;
using VillaProject_WEB.Models.VM.Interface;
using VillaProject_WEB.Services.IServices;

namespace VillaProject_WEB.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _numberService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService numberService, IVillaService villaService, IMapper mapper)
        {
            _numberService = numberService;
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();

            var response = await _numberService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                var result = Convert.ToString(response.Result);
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(result);
            }
            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new(await GetVillaNumberVM());

            return View(villaNumberVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _numberService.CreateAsync<APIResponse>(model.VillaNumber);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            model.VillaList = await GetVillaNumberVM();
            return View(model);
        }

        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
 
            VillaNumberUpdateVM villaNumberUpdateVM = new(await GetVillaNumberVM());
            var response = await _numberService.GetAsync<APIResponse>(villaNo);

            if (response != null && response.IsSuccess)
            {
                var dto = Convert.ToString(response.Result);

                villaNumberUpdateVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(JsonConvert.DeserializeObject<VillaNumberDTO>(dto));
                return View(villaNumberUpdateVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {

            if (ModelState.IsValid)
            {
                var response = await _numberService.UpdateAsync<APIResponse>(model.VillaNumber);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {
            var response = await _numberService.GetAsync<APIResponse>(villaNo);

            if (response != null && response.IsSuccess)
            {
                var dto = Convert.ToString(response.Result);
                VillaNumberDeleteDTO model = JsonConvert.DeserializeObject<VillaNumberDeleteDTO>(dto);
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteDTO model)
        {

            var response = await _numberService.DeleteAsync<APIResponse>(model.VillaNo);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            return View(model);
        }

        private async Task<IEnumerable<SelectListItem>> GetVillaNumberVM()
        {
            var response = await _villaService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                var villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(
                    Convert.ToString(response.Result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return villaList;
            }
            return Enumerable.Empty<SelectListItem>();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaProject_WEB.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Services.IServices;

namespace VillaProject_WEB.Controllers
{
	public class VillaController : Controller
	{
		private readonly IVillaService _villaService;
		private readonly IMapper _mapper;
		public VillaController(IVillaService villaService, IMapper mapper)
		{
			_mapper = mapper;
			_villaService = villaService;
		}
		public async Task<IActionResult> IndexVilla()
		{
			List<VillaDTO> list = new();

			var response = await _villaService.GetAllAsync<APIResponse>();

			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		public async Task<IActionResult> CreateVilla()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _villaService.CreateAsync<APIResponse>(model);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexVilla));
				}
			}
			return View(model);
		}

		public async Task<IActionResult> UpdateVilla(int VillaId)
		{
			var response = await _villaService.GetAsync<APIResponse>(VillaId);

			if (response != null && response.IsSuccess)
			{
				var dto = Convert.ToString(response.Result);
				VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(dto);
				return View(_mapper.Map<VillaUpdateDTO>(model));
			}
			return NotFound();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
		{

			if (ModelState.IsValid)
			{
				var response = await _villaService.UpdateAsync<APIResponse>(model);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexVilla));
				}
			}
			return View(model);
		}

		public async Task<IActionResult> DeleteVilla(int VillaId)
		{
			var response = await _villaService.GetAsync<APIResponse>(VillaId);

			if (response != null && response.IsSuccess)
			{
				var dto = Convert.ToString(response.Result);
				VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(dto);
				return View(model);
			}
			return NotFound();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteVilla(VillaDTO model)
		{

				var response = await _villaService.DeleteAsync<APIResponse>(model.Id);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexVilla));
				}
			return View(model);
		}
	}
}

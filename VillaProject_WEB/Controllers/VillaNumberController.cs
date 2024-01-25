using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using VillaProject_WEB.Models;
using VillaProject_WEB.Models.DTO;
using VillaProject_WEB.Services;
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
			ViewBag.Villas = new SelectList(items: await GetVillasSelectList(), dataValueField: "Id", dataTextField: "Name");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _numberService.CreateAsync<APIResponse>(model);

				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexVillaNumber));
				}
			}
			return View(model);
		}
		private async Task<IEnumerable<object>> GetVillasSelectList()
		{
			List<VillaDTO> list = new();
			var response = await _villaService.GetAllAsync<APIResponse>();

			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
			}
			var result = list.Select(s => new { s.Id, s.Name }).ToList();
			return result;
		}
	}
}

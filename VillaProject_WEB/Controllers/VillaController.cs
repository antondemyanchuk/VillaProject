using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaProject_Utility;
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

			var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
			}
			return View(list);
		}

		[Authorize(Roles ="admin")]
		public async Task<IActionResult> CreateVilla()
		{
			return View();
		}
		[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Number created successfully";
					return RedirectToAction(nameof(IndexVilla));
				}
			}
			TempData["error"] = "Error encountered";
			return View(model);
		}

		[Authorize(Roles = "admin")]
		public async Task<IActionResult> UpdateVilla(int villaId)
		{
			var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));

			if (response != null && response.IsSuccess)
			{
				var dto = Convert.ToString(response.Result);
				VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(dto);
				return View(_mapper.Map<VillaUpdateDTO>(model));
			}
			return NotFound();
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
		{

			if (ModelState.IsValid)
			{
				var response = await _villaService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Number created successfully";
					return RedirectToAction(nameof(IndexVilla));
				}
			}
			TempData["error"] = "Error encountered";
			return View(model);
		}

		[Authorize(Roles = "admin")]
		public async Task<IActionResult> DeleteVilla(int villaId)
		{
			var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));

			if (response != null && response.IsSuccess)
			{
				var dto = Convert.ToString(response.Result);
				VillaDTO model = JsonConvert.DeserializeObject<VillaDTO>(dto);
				return View(model);
			}
			return NotFound();
		}
		[Authorize(Roles = "admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteVilla(VillaDTO model)
		{

			var response = await _villaService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));

			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Number created successfully";
				return RedirectToAction(nameof(IndexVilla));
			}
			TempData["error"] = "Error encountered";
			return View(model);
		}
	}
}

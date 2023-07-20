﻿namespace CarRepairGarage.Web.Controllers
{
    using CarRepairGarage.Services.Contracts;

    using Microsoft.AspNetCore.Mvc;

    public class GarageController : Controller
    {
        private readonly IGarageService _garageService;
        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _garageService.GetAllGaragesAsync(int.MaxValue);
            return View(model);
        }
    }
}

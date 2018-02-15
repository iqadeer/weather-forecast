﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BOA.WeatherForcast.Web.Services;
using BOA.WeatherForcast.Web.ViewModels;
using BOA.WeatherForecast.Data;
using BOA.WeatherForecast.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BOA.WeatherForcast.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ForecastController : Controller
    {
        private readonly ILogger<ForecastController> _logger;
        private readonly IForecastService _forecastService;

        public ForecastController(ILogger<ForecastController> logger, IForecastService forecastService)
        {
            _logger = logger;
            _forecastService = forecastService;
        }

        [HttpGet]
        public IActionResult Get(string country = "GB")
        {
            try
            {
                return Ok(_forecastService.GetCities(country));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to get list of supported cities.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                    return Ok(await _forecastService.GetWeatherForecast(id));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return BadRequest("Failed to get list of supported cities.");
            }
        }

        private void LogError(Exception ex)
        {
            _logger.LogError($"Error getting cities for the file, Error: {ex.Message} Exception type: {ex.GetType()}.");
        }

    }
}
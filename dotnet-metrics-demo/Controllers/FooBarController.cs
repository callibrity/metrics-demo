using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_metrics_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FooBarController : ControllerBase
    {
        private readonly ILogger<FooBarController> _logger;

        public FooBarController(ILogger<FooBarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rng = new Random();
            var isError = rng.Next(100) == 99;

            if (isError)
            {
                await Task.Delay(rng.Next(20, 100));
                return StatusCode(500);
            }

            var isOutlier = rng.Next(0, 100) >= 94;

            if (isOutlier)
            {
                await Task.Delay(rng.Next(800, 1500));
            }
            else
            {
                await Task.Delay(rng.Next(200, 700));
            }

            return StatusCode(200);
        }
    }
}

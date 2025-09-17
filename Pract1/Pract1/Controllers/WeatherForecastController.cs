using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Pract1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

      
        [HttpGet("GetAll")]
        public IActionResult GetAll(int? sortStrategy = null)
        {
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }
            if (sortStrategy == 1)
            {
                var sortedAsc = Summaries.OrderBy(s => s).ToList();
                return Ok(sortedAsc);
            }
            if (sortStrategy == -1)
            {
                var sortedDesc = Summaries.OrderByDescending(s => s).ToList();
                return Ok(sortedDesc);
            }
            return BadRequest("Некорректное значение параметра sortStrategy");
        }

  
        [HttpGet("GetOne")]
        public IActionResult GetOne(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный");
            }
            return Ok(Summaries[index]);
        }

  
        [HttpGet("GetCountByName")]
        public IActionResult GetCountByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Имя не может быть пустым");
            }
            int count = Summaries.Count(s => s.Equals(name));
            return Ok(count);
        }


        [HttpPost]
        public IActionResult Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Имя не может быть пустым");
            }

            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Имя не может быть пустым");
            }

            Summaries[index] = name;
            return Ok();
        }

       
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }
    }
}
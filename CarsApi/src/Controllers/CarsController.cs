using System.Collections.Generic;
using System.Linq;
using CarsApi.Messages;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IEnumerable<CarDto> _cars = new List<CarDto>
        {
            new CarDto
            {
                Id = 1,
                Brand = "Volkswagen",
                Model = "Golf",
                Color = "Black"
            },
            new CarDto
            {
                Id = 2,
                Brand = "Mercedes-Benz",
                Model = "A200",
                Color = "Grey"
            },
            new CarDto
            {
                Id = 3,
                Brand = "Audi",
                Model = "Q5",
                Color = "Black"
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> Get() 
            => Ok(_cars);
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CarDto>> GetById(int id) 
            => Ok(_cars.SingleOrDefault(c => c.Id == id));
    }
}

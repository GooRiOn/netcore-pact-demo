using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Provider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet("{id}")]
        public Data GetData(Guid id)
            => new Data
            {
                Id = id,
                Value1 = "Totally",
                Value2 = 2
            };
    }
    
    public class Data
    {
        public Guid Id { get; set; }
        public string Value1 { get; set; }
        public int Value2 { get; set; }
    }
}

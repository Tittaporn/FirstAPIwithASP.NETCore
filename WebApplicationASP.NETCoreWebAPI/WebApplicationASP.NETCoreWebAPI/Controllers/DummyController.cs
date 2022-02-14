using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationASP.NETCoreWebAPI.Contexts;

namespace WebApplicationASP.NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/testdatabase")]
    public class DummyController : ControllerBase 
    {
        private readonly CityInfoContext _ctx;
        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            this._context = context;
        }

        // GET api/values
       
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values= await _context.Values.ToListAsync();

            return Ok(values);
        }


        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult Get([FromQuery]Value value)
        //{
        //    return Ok(new Value { id = value.id, text = value.text });
        //}

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue (int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x=> x.id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction("Get", new { id = value.id }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }

    public class Value
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}

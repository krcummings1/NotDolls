using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using NotDolls.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotDolls.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("AllowDevelopmentEnvironment")]
    public class InventoryImageController : Controller
    {

        private NotDollsContext _context;

        public InventoryImageController(NotDollsContext context)
        {
            _context = context;
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<object> inventoryImage = from i in _context.InventoryImage
                                      select i;

            if (inventoryImage == null)
            {
                return NotFound();
            }

            return Ok(inventoryImage);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetInventoryImage")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InventoryImage inventoryImage = _context.InventoryImage.Single(m => m.InventoryImageId == id);

            if (inventoryImage == null)
            {
                return NotFound();
            }

            return Ok(inventoryImage);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]InventoryImage inventoryImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InventoryImage.Add(inventoryImage);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InventoryImageExists(inventoryImage.InventoryImageId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInventoryImage", new { id = inventoryImage.InventoryImageId }, inventoryImage); // "GetInventoryImage" matches the Name of the Get(int id) method
        }

        // PUT api/values/5
        [HttpPut("{id}", Name = "GetInventoryImage")]
        public IActionResult Put(int id, [FromBody]InventoryImage inventoryImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventoryImage.InventoryImageId)
            {
                return BadRequest();
            }

            _context.Entry(inventoryImage).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInventoryImage", new { id = inventoryImage.InventoryImageId }, inventoryImage);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInventoryImage(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InventoryImage inventoryImage = _context.InventoryImage.Single(m => m.InventoryImageId == id);
            if (inventoryImage == null)
            {
                return NotFound();
            }

            _context.InventoryImage.Remove(inventoryImage);
            _context.SaveChanges();

            return Ok(inventoryImage);
        }

        private bool InventoryImageExists(int id)
        {
            return _context.InventoryImage.Count(e => e.InventoryImageId == id) > 0;
        }

    }
}

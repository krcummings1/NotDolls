﻿using System;
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
    public class InventoryController : Controller
    {

        private NotDollsContext _context;

        public InventoryController(NotDollsContext context)
        {
            _context = context;
        }


        // GET: api/values
        [HttpGet]   // ********   adding QUERY parameters to be able to FILTER results from API    *************
        public IActionResult Get([FromQuery]int? GeekId, [FromQuery]int? Year) // locahost:5000/api/Inventory?GeekId=2 >> filter inventory where GeekId = 2
        {                                                                       // locahost:5000/api/Inventory?Year=1990&GeekId=2
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Inventory> inventory = from i in _context.Inventory
                                           select i;
            if(Year != null)
            {
                inventory = inventory.Where(inv => inv.Year == Year);
            }

            //if (GeekId != null)
            //{
            //    inventory = inventory.Where(inv => inv.GeekId == GeekId);
            //}

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory); // send response header http code 200
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetInventory")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory inventory = _context.Inventory.Single(m => m.InventoryId == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Inventory.Add(inventory);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InventoryExists(inventory.InventoryId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInventory", new { id = inventory.InventoryId }, inventory);
        }

        // PUT api/values/5
        [HttpPut("{id}", Name = "GetInventory")]
        public IActionResult Put(int id, [FromBody]Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.InventoryId)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetInventory", new { id = inventory.InventoryId }, inventory);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInventory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inventory inventory = _context.Inventory.Single(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventory.Remove(inventory);
            _context.SaveChanges();

            return Ok(inventory);
        }


        private bool InventoryExists(int id)
        {
            return _context.Inventory.Count(e => e.InventoryId == id) > 0;
        }

    }
}

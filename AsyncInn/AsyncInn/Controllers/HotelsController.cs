﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Data.Services;
using AsyncInn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        //private readonly HotelDbContext _context;
        IHotelService hotelService;

        public HotelsController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        // GET: api/Hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
            return Ok(await hotelService.GetAllHotels());
           // return await _context.Hotel.ToListAsync();
        }

        // GET: api/Hotel/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Hotel>> GetHotel(int ID)
        {
            //var hotel = await _context.Hotel.FindAsync(ID);
            var hotel = await hotelService.GetOneHotel(ID);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.ID)
            {
                return BadRequest();
            }

            bool didUpdate = await hotelService.UpdateHotel(hotel);

            if (!didUpdate)
                return NotFound();

            //_context.Entry(hotel).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!HotelExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Hotel
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await hotelService.AddHotel(hotel);
            //_context.Hotel.Add(hotel);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.ID }, hotel);
        }

        // DELETE: api/Hotel/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int ID)
        {
            var hotel = await hotelService.DeleteHotel(ID);
            //var hotel = await _context.Hotel.FindAsync(ID);
            if (hotel == null)
            {
                return NotFound();
            }

            //_context.Hotel.Remove(hotel);
            //await _context.SaveChangesAsync();

            return hotel;
        }

        private bool HotelExists(int ID)
        {
            return false;
            //return _context.Hotel.Any(e => e.ID == ID);
        }
    }
}

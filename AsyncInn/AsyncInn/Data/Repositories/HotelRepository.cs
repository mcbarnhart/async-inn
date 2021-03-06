﻿using AsyncInn.Models;
using AsyncInn.Models.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data.Services
{
    public class HotelRepository : IHotelRepository
    {
        private HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HotelDTO>> GetAllHotels()
        {
            var hotel = await _context.Hotel
                .Select(hotel => new HotelDTO
                {
                    ID = hotel.ID,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Country = hotel.Country,
                    Phone = hotel.Phone,

                    Rooms = hotel.HotelRoom
                    .Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelID,
                        Number = hr.Number,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFriendly,

                        Room = new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = hr.Room.Layout.ToString(),

                            Amenities = hr.Room.Amenities
                            .Select(ra => new AmenityDTO
                            {
                                ID = ra.Amenity.ID,
                                Name = ra.Amenity.Name
                            })
                            .ToList()
                        }
                    })
                    .ToList()
                })
                .ToListAsync();

            return hotel;

        }
        public async Task<HotelDTO> GetOneHotel(int ID)
        {
            return await _context.Hotel
                .Select(hotel => new HotelDTO
                {
                    ID = hotel.ID,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Country = hotel.Country,
                    Phone = hotel.Phone,

                    Rooms = hotel.HotelRoom
                    .Select(hr => new HotelRoomDTO
                    {
                        HotelID = hr.HotelID,
                        Number = hr.Number,
                        Rate = hr.Rate,
                        PetFriendly = hr.PetFriendly,

                        Room = new RoomDTO
                        {
                            ID = hr.Room.ID,
                            Name = hr.Room.Name,
                            Layout = hr.Room.Layout.ToString(),

                            Amenities = hr.Room.Amenities
                            .Select(ra => new AmenityDTO
                            {
                                ID = ra.Amenity.ID,
                                Name = ra.Amenity.Name
                            })
                            .ToList()
                        }
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync(hotel => hotel.ID == ID);

        }

        //public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(int hotelID)
        //{
        //    var rooms = await _context.HotelRooms
        //        .Where(HotelRepository =>)
        //}

        public async Task<bool> UpdateHotel(int ID, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Hotel> AddHotel(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> DeleteHotel(int ID)
        {
            var hotel = await _context.Hotel.FindAsync(ID);
            if (hotel == null)
            {
                return null;
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }
        private bool HotelExists(int ID)
        {
            return _context.Hotel.Any(e => e.ID == ID);
        }


    }
}

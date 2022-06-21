using System;
using System.Collections.Generic;
using System.Linq;
using RoomBooking.Core.DataServices;
using RoomBooking.Core.Domain;

namespace RoomBooking.Persistence.Repositories
{
  public class RoomBookingService : IRoomBookingService
  {
    private readonly RoomBookingAppDbContext _context;

    public RoomBookingService(RoomBookingAppDbContext context)
    {
      this._context = context;
    } 

    public IEnumerable<Room> GetAvailableRooms(DateTime date)
    {
      return _context.Rooms.Where(q => !q.RoomBookings.Any(x => x.Date == date)).ToList();
    }

    public void Save(Booking booking)
    {
      _context.Add(booking);
      _context.SaveChanges();
    }
  }
}


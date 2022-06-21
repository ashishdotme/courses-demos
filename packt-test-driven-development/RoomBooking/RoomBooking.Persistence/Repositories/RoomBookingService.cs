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
      var unAvailableRooms = _context.RoomBookings.Where(q => q.Date == date).Select(q => q.RoomId);
      var availableRooms = _context.Rooms.Where(q => unAvailableRooms.Contains(q.Id) == false).ToList();
      return availableRooms;
    }

    public void Save(Booking booking)
    {
      throw new NotImplementedException();
    }
  }
}


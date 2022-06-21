using System;
using System.Collections.Generic;
using RoomBooking.Core.Domain;

namespace RoomBooking.Core.DataServices
{
  public interface IRoomBookingService
  {
    void Save(Booking booking);

    IEnumerable<Room> GetAvailableRooms(DateTime date);
  }
}


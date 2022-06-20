using System;
using System.Collections.Generic;

namespace RoomBooking.Core.DataServices
{
  public interface IRoomBookingService
  {
    void Save(Booking booking);

    IEnumerable<Room> GetAvailableRooms(DateTime date);
  }
}


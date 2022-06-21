using System;

namespace RoomBooking.Core.Domain
{
  public class Booking : RoomBookingBase
  {
    public int RoomId { get; set; }
    public int Id { get; set; }
    public Room Room { get; set; }
  }
}
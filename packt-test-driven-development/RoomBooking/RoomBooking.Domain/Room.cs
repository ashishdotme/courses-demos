using System.Collections.Generic;

namespace RoomBooking.Core.Domain
{
  public class Room
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Booking> RoomBookings { get; set; }
  }
}
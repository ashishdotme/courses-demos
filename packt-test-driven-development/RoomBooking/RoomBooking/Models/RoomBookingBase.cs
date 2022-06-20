using System;

namespace RoomBooking.Core.Models
{
  public abstract class RoomBookingBase
  {
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
  }
}
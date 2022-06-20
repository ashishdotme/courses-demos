using System;
using RoomBooking.Core.Models;

namespace RoomBooking.Core.DataServices
{
  public class Booking : RoomBookingBase
  {
    public int RoomId { get; internal set; }
    public int Id { get; set; }
  }
}
using System;
using RoomBooking.Core.Enums;
using RoomBooking.Core.Models;

namespace RoomBooking.Core
{
  public class RoomBookingResult : RoomBookingBase
  {
    public BookingResultFlag Flag { get; set; }
    public object RoomBookingId { get; set; }
  }
}
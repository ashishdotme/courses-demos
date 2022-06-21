using System;
using RoomBooking.Core.Domain;
using RoomBooking.Core.Enums;

namespace RoomBooking.Core
{
  public class RoomBookingResult : RoomBookingBase
  {
    public BookingResultFlag Flag { get; set; }
    public object RoomBookingId { get; set; }
  }
}
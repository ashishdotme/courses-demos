namespace RoomBooking.Core
{
  public interface IRoomBookingRequestProcessor
  {
    RoomBookingResult BookRoom(RoomBookingRequest bookingRequest);
  }
}
using System;
using System.Linq;
using RoomBooking.Core;
using RoomBooking.Core.DataServices;
using RoomBooking.Core.Models;

namespace RoomBooking.Core
{
  public class RoomBookingRequestProcessor
  {
    private IRoomBookingService _roomBookingService;

    public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
    {
      this._roomBookingService = roomBookingService;

    }

    public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
    {
      if (bookingRequest == null)
      {
        throw new ArgumentNullException(nameof(bookingRequest));
      }

      var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);

      if (availableRooms.Any())
      {
        _roomBookingService.Save(CreateRoomBookingObject<Booking>(bookingRequest));
      }

      return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
    }

    private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking : RoomBookingBase, new()
    {
      return new TRoomBooking
      {
        FullName = bookingRequest.FullName,
        Date = bookingRequest.Date,
        Email = bookingRequest.Email
      };
    }
  }
}
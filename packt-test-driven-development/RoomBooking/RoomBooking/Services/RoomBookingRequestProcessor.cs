using System;
using System.Linq;
using RoomBooking.Core;
using RoomBooking.Core.DataServices;
using RoomBooking.Core.Enums;
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
      var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

      if (availableRooms.Any())
      {
        var room = availableRooms.First();
        var roomBooking = CreateRoomBookingObject<Booking>(bookingRequest);
        roomBooking.RoomId = room.Id;
        _roomBookingService.Save(roomBooking);

        result.RoomBookingId = roomBooking.Id;
        result.Flag = BookingResultFlag.Success;

      }
      else
      {
        result.Flag = BookingResultFlag.Failure;
      }

      return result;
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
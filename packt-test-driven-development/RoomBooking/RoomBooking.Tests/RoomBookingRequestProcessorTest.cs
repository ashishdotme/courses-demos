using RoomBooking.Core;
using Xunit;
using System;
using Shouldly;
using RoomBooking.Core.DataServices;
using RoomBooking.Core.Enums;
using Moq;
using System.Collections.Generic;
using RoomBooking.Core.Domain;

namespace RoomBooking.Tests
{
  public class RoomBookingRequestProcessorTest
  {

    private RoomBookingRequestProcessor _processor;
    private RoomBookingRequest _request;
    private Mock<IRoomBookingService> _roomBookingServiceMock;
    private List<Room> _availableRooms;

    public RoomBookingRequestProcessorTest()
    {
      // Arrange
      _request = new RoomBookingRequest
      {
        FullName = "Test Name",
        Email = "test@request.com",
        Date = new DateTime(2021, 11, 22)
      };


      _availableRooms = new List<Room> { new Room() { Id = 1} };

      _roomBookingServiceMock = new Mock<IRoomBookingService>();
      _roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_request.Date)).Returns(_availableRooms);
      _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

    }

    [Fact]
    public void Should_Return_Room_Booking_Response_With_Request_Values()
    {

      // Act
      RoomBookingResult result = _processor.BookRoom(_request);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(result.FullName, _request.FullName);

    }

    [Fact]
    public void Should_Throw_Exception_For_Null_Request()
    {
      var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
      exception.ParamName.ShouldBe("bookingRequest");
    }

    [Fact]
    public void Should_Save_Room_Booking_Request()
    {
      Booking savedBooking = null;
      _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<Booking>()))
        .Callback<Booking>(booking =>  
        {
          savedBooking = booking;
        });
      _processor.BookRoom(_request);

      _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<Booking>()), Times.Once);

      savedBooking.ShouldNotBeNull();
      savedBooking.FullName.ShouldBe(_request.FullName);
      savedBooking.Date.ShouldBe(_request.Date);
    }

    [Fact] 
    public void Should_Not_Save_Room_Booking_Request_If_None_Available()
    {
      _availableRooms.Clear();
      _processor.BookRoom(_request);
      _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<Booking>()), Times.Never);
    }

    [Theory]
    [InlineData(BookingResultFlag.Failure, false)]
    [InlineData(BookingResultFlag.Success, true)]
    public void Should_Return_SuccessOrFailure_Flag_In_Result(BookingResultFlag bookingSuccessFlag, bool isAvailable)
    {
      if (!isAvailable)
      {
        _availableRooms.Clear();
      }

      var result = _processor.BookRoom(_request);
      bookingSuccessFlag.ShouldBe(result.Flag);

    }


    [Theory]
    [InlineData(1, true)] 
    [InlineData(null, false)]
    public void Should_Return_RoomBookingId_In_Result(int? roomBookingId, bool isAvailable)
    {
      if (!isAvailable)
      {
        _availableRooms.Clear();
      }
      else
      {
        _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<Booking>()))
        .Callback<Booking>(booking =>
        {
          booking.Id = roomBookingId.Value;
        });

        var result =  _processor.BookRoom(_request);
        result.RoomBookingId.ShouldBe(roomBookingId);
      }

    }
  }
}


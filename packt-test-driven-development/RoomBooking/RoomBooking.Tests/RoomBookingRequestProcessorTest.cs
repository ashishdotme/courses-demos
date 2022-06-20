using RoomBooking.Core;
using Xunit;
using System;
using Shouldly;
using RoomBooking.Core.DataServices;
using Moq;

namespace RoomBooking.Tests
{
  public class RoomBookingRequestProcessorTest
  {

    private RoomBookingRequestProcessor _processor;
    private RoomBookingRequest _request;
    private Mock<IRoomBookingService> _roomBookingServiceMock;

    public RoomBookingRequestProcessorTest()
    {
      // Arrange

      _roomBookingServiceMock = new Mock<IRoomBookingService>();

      _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

      _request = new RoomBookingRequest
      {
        FullName = "Test Name",
        Email = "test@request.com",
        Date = new DateTime(2, 2, 2)
      };

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

  }
}


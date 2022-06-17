using RoomBooking.Core;
using Xunit;


namespace RoomBooking.Tests
{
  public class RoomBookingRequestProcessorTest
  {

    [Fact]
    public void Should_Return_Room_Booking_Response_With_Request_Values()
    {

      // Arrange
      var bookingRequest = new RoomBookingRequest
      {
        FullName = "Test Name",
        Email = "test@request.com"
      };
      var processor = new RoomBookingRequestProcessor();

      // Act
      RoomBookingResult result = processor.BookRoom(bookingRequest);

      // Assert
      Assert.NotNull(result);

    }

  }
}


using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using RoomBooking.API.Controllers;
using System.Linq;
using Shouldly;

namespace RoomBooking.API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Return_Forcast_Results()
        {
          var loggerMock = new Mock<ILogger<WeatherForecastController>>();
          var controller = new WeatherForecastController(loggerMock.Object);

          var result = controller.Get();

          result.Count().ShouldBeGreaterThan(1);
          result.ShouldNotBeNull();
        }
    }
}


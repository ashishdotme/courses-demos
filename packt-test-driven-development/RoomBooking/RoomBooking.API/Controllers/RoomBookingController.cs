using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomBooking.Core;
using RoomBooking.Core.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoomBooking.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RoomBookingController : ControllerBase
  {
    private IRoomBookingRequestProcessor _roomBookingProcessor;

    public RoomBookingController(IRoomBookingRequestProcessor roomBookingProcessor)
    {
      this._roomBookingProcessor = roomBookingProcessor;
    }

    [HttpPost]
    public async Task<IActionResult> BookRoom(RoomBookingRequest request)
    {
      if (ModelState.IsValid)
      {
        var result = _roomBookingProcessor.BookRoom(request);
        if (result.Flag == BookingResultFlag.Success)
        {
          return Ok(result);
        }
        ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No Rooms Available For Given Date");
      }
      return BadRequest(ModelState);
    }
  }

}
using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;


// ASP.NET core aplication - wraps the API controllers and adds some built in functionality

public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost("")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfastObject = new BreakfastModel(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);

        _breakfastService.CreateBreakfast(breakfastObject);

        var response = new BreakfastResponse(
            breakfastObject.Id,
            breakfastObject.Name,
            breakfastObject.Description,
            breakfastObject.StartDateTime,
            breakfastObject.EndDateTime,
            breakfastObject.LastModifiedDateTime,
            breakfastObject.Savory,
            breakfastObject.Sweet);

        return CreatedAtAction(actionName: nameof(GetBreakfast), routeValues: new { id = response.Id }, value: response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<BreakfastModel> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors));
    }

    private static BreakfastResponse MapBreakfastResponse(BreakfastModel breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet);
    }



    [HttpPut("{id:guid}")]
    public IActionResult UpdateBreakfast(Guid id, UpdateBreakfastRequest request)
    {
        var breakfastObject = new BreakfastModel(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);

        _breakfastService.UpdateBreakfast(breakfastObject);

        // TODO: Return 201 if new breakfast created


        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }




}


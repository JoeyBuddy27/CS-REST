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

    // Create POST
    [HttpPost("")]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var requestToBreakfastResult = BreakfastModel.From(request);



        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfastObject = requestToBreakfastResult.Value;
        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfastObject);


        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfastObject),
            errors => Problem(errors)
        );
    }



    // GET
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<BreakfastModel> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors));
    }



    // Update PUT
    [HttpPut("{id:guid}")]
    public IActionResult UpdateBreakfast(Guid id, UpdateBreakfastRequest request)
    {
        var requestToBreakfastResult = BreakfastModel.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfastObject = requestToBreakfastResult.Value;


        ErrorOr<UpdatedBreakfast> updatedBreakfastResult = _breakfastService.UpdateBreakfast(breakfastObject);

        // TODO: Return 201 if new breakfast created

        return updatedBreakfastResult.Match(
            upserted => upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfastObject) : NoContent(),
            errors => Problem(errors)
        );

    }


    // DELETE
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedBreakfastResult = _breakfastService.DeleteBreakfast(id);


        return deletedBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
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


    private CreatedAtActionResult CreatedAtGetBreakfast(BreakfastModel breakfast)
    {
        return CreatedAtAction(actionName: nameof(GetBreakfast), routeValues: new { id = breakfast.Id }, value: MapBreakfastResponse(breakfast));
    }

}


using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;
public class BreakfastService : IBreakfastService
{

    // dictionary is a collection of key/value pairs
    private static readonly Dictionary<Guid, BreakfastModel> _breakfasts = new();
    public ErrorOr<Created> CreateBreakfast(BreakfastModel breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
        return Result.Deleted;

    }

    public ErrorOr<BreakfastModel> GetBreakfast(Guid id)
    {
        if (_breakfasts.ContainsKey(id))
        {
            return _breakfasts[id];
        }
        else
        {
            return Errors.Breakfast.NotFound(id);
        }
    }

    public ErrorOr<UpdatedBreakfast> UpdateBreakfast(BreakfastModel breakfast)
    {
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
        _breakfasts[breakfast.Id] = breakfast;

        return new UpdatedBreakfast(isNewlyCreated);
    }


}
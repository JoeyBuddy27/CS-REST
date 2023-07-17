using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;
public class BreakfastService : IBreakfastService
{

    // dictionary is a collection of key/value pairs
    private static readonly Dictionary<Guid, BreakfastModel> _breakfasts = new();
    public void CreateBreakfast(BreakfastModel breakfast)
    {
        // throw new NotImplementedException();
        _breakfasts.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
        // throw new NotImplementedException();

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

    public void UpdateBreakfast(BreakfastModel breakfast)
    {
        // throw new NotImplementedException();
        _breakfasts[breakfast.Id] = breakfast;
    }


}
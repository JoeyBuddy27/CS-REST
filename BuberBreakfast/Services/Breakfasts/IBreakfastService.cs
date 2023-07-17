using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    void CreateBreakfast(BreakfastModel breakfast);
    ErrorOr<BreakfastModel> GetBreakfast(Guid id);
    void UpdateBreakfast(BreakfastModel breakfast);
    void DeleteBreakfast(Guid id);
}
using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    // Created etc comes from ErrorOr library
    ErrorOr<Created> CreateBreakfast(BreakfastModel breakfast);
    ErrorOr<BreakfastModel> GetBreakfast(Guid id);
    ErrorOr<UpdatedBreakfast> UpdateBreakfast(BreakfastModel breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
}
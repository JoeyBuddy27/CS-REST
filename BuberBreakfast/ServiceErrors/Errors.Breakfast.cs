using BuberBreakfast.Models;
using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {

        // custom validation error objects
        public static Error InvalidName => Error.Validation(
         code: "Breakfast.InvalidName",
         description: $"The breakfast name must be at least {BreakfastModel.MinNameLength} characters long and no greater than {BreakfastModel.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
         code: "Breakfast.InvalidDescription",
         description: $"The breakfast description must be at least {BreakfastModel.MinDescriptionLength} characters long and no greater than {BreakfastModel.MaxDescriptionLength} characters long.");

        // custom not found object
        public static Error NotFound(Guid id) => Error.NotFound(
            code: "breakfast/not-found",
            description: $"The breakfast {id} was not found.");
    }
}
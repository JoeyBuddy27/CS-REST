using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound(Guid id) => Error.NotFound(
            code: "breakfast/not-found",
            description: $"The breakfast {id} was not found.");
    }
}
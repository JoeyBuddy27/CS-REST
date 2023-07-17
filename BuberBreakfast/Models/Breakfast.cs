using BuberBreakfast.ServiceErrors;
using ErrorOr;
using BuberBreakfast.Contracts.Breakfast;

namespace BuberBreakfast.Models;

public class BreakfastModel
{

    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 3;
    public const int MaxDescriptionLength = 500;
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }


    // constructor
    private BreakfastModel(Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)
    {
        // enforce invariants
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    // enforce typing rules
    public static ErrorOr<BreakfastModel> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savory,
        List<string> sweet,
        Guid? id = null)
    {
        // enforce rules on paramters
        // var id = Guid.NewGuid();
        var lastModifiedDateTime = DateTime.UtcNow;

        List<Error> errors = new();

        if (name.Length < MinNameLength || name.Length > MaxNameLength)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }

        if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }

        if (errors.Count > 0)
        {
            return errors;
        }


        return new BreakfastModel(
            id ?? Guid.NewGuid(),
            name,
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            savory,
            sweet);
    }

    public static ErrorOr<BreakfastModel> From(CreateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet
        );
    }

    public static ErrorOr<BreakfastModel> From(Guid id, UpdateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Savory,
            request.Sweet,
            id
        );
    }

}
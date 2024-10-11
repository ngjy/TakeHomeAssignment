using FluentValidation;

public class PeopleValidator : AbstractValidator<Person>
{
    public PeopleValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
        RuleFor(p => p.Phone).Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits");
    }
}
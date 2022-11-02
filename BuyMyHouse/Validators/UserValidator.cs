using FluentValidation;
using Models;

namespace BuyMyHouse.Validators;

public class UserValidator : AbstractValidator<User> 
{
    public UserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Income).NotEmpty();
    }
}
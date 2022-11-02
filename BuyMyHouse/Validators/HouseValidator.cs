using FluentValidation;
using Models;

namespace BuyMyHouse.Validators;

public class HouseValidator : AbstractValidator<House>
{
    public HouseValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
    }
}
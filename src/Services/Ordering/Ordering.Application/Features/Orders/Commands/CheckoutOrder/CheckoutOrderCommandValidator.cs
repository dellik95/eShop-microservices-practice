using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
	public CheckoutOrderCommandValidator()
	{
		RuleFor(o => o.UserName)
			.NotEmpty()
			.WithMessage("UserName Is required.")
			.NotNull()
			.MaximumLength(24).WithMessage("Max length is 24");

		RuleFor(o => o.EmailAddress)
			.NotEmpty().WithMessage("Email address is required.");


		RuleFor(o => o.TotalPrice)
			.NotEmpty().WithMessage("Total price should be entered")
			.GreaterThan(0).WithMessage("Total price can not be ZERO");
	}
}
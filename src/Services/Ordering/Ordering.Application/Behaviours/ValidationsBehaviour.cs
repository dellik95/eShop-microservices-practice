using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours;

public class ValidationsBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> validators;

	public ValidationsBehaviour(IEnumerable<IValidator<TRequest>> validators)
	{
		this.validators = validators;
	}


	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		if (!validators.Any()) return await next();
		var validationContext = new ValidationContext<TRequest>(request);
		var validationResult =
			await Task.WhenAll(validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));
		var failures = validationResult.SelectMany(x => x.Errors).Where(x => x != null).ToList();
		if (failures.Any())
		{
			throw new ValidationException(failures);
		}

		return await next();
	}
}
using FluentValidation.Results;

namespace Ordering.Application.Exceptions;

public class ValidationException : ApplicationException
{
	public ValidationException() : base("One or more validation failures occured.")
	{
		Errors = new Dictionary<string, string[]>();
	}


	public ValidationException(IEnumerable<ValidationFailure> failures) : base(
		"One or more validation failures occured.")
	{
		Errors = failures
			.GroupBy(f => f.PropertyName, e => e.ErrorMessage)
			.ToDictionary(e => e.Key, e => e.ToArray());
	}

	public Dictionary<string, string[]> Errors { get; }
}
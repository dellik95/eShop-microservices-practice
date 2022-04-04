namespace Ordering.Application.Exceptions;

public class NotFoundException : ApplicationException
{
	public NotFoundException(string name, object key) : base($"Entity {name} with {key} not exists")
	{
	}
}
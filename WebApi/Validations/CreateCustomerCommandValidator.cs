using FluentValidation;
using WebApi.Commands;

namespace WebApi.Validations
{
	public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
	{
		public CreateCustomerCommandValidator()
		{
			RuleFor(x => x.Customer)
				.NotNull();

			RuleFor(x => x.Customer.FullName)
				.NotEmpty();
		}
	}
}

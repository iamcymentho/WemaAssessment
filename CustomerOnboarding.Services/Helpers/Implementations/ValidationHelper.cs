using CustomerOnboarding.Domain.DataTransferObjects.DtoValidators;
using CustomerOnboarding.Domain.DataTransferObjects;
// ValidationHelper.cs  
using FluentValidation;
using FluentValidation.Results;
using CustomerOnboarding.Services.Helpers.Interfaces;
// ValidationHelper.cs  
using System.Threading.Tasks;

namespace CustomerOnboarding.Services.Helpers.Implementations
{
    public class ValidationHelper : IValidationHelper
    {
        private readonly IValidator<CustomerDto> _customerValidator;

        public ValidationHelper()
        {
            _customerValidator = new CustomerValidator(); 
        }

        public async Task ValidateCustomerAsync(CustomerDto customerDto)
        {
            ValidationResult result = await _customerValidator.ValidateAsync(customerDto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}

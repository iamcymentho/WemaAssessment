using CustomerOnboarding.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Data.Repositories.Interfaces
{
     public interface ICustomerRepository
    {
        Task AddCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<bool> IsValidStateAndLGAAsync(string state, string lga);
        Task<Customer> GetByPhoneNumberAsync(string phoneNumber);
    }
}

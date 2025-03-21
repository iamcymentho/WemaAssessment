using CustomerOnboarding.Data.Repositories.Interfaces;
using CustomerOnboarding.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace CustomerOnboarding.Data.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerOnboardingDbContext _context;
        private readonly Dictionary<string, List<string>> _stateLgaMap;

        public CustomerRepository(CustomerOnboardingDbContext context, IConfiguration configuration)
        {
            _context = context;
            _stateLgaMap = configuration
                .GetSection("StateLGAMap")   // Access the "StateLGAMap" section  
                .Get<Dictionary<string, List<string>>>() ?? new Dictionary<string, List<string>>();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public Task<bool> IsValidStateAndLGAAsync(string state, string lga)
        {
            return Task.FromResult(_stateLgaMap.TryGetValue(state, out var lgas) && lgas.Contains(lga));
        }
    }
}

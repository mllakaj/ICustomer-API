using ICustomer_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ICustomer_API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ICustomerDbContext _dbContext;

        public CustomerRepository(ICustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task AddCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                throw new ArgumentException("Id mismatch");
            }

            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    throw new KeyNotFoundException("Customer not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        private bool CustomerExists(int id)
        {
            return _dbContext.Customers.Any(e => e.Id == id);
        }

    }
}


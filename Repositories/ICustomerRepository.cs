using ICustomer_API.Models;

namespace ICustomer_API.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(int id);
        Task<List<Customer>> GetCustomers();
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(int id, Customer customer);
        Task DeleteCustomer(int id);
    }
   
}

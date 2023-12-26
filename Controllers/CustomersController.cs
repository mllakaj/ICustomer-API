using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ICustomer_API.Models;
using ICustomer_API.Repositories;

namespace ICustomer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomHttpClient _customHttpClient;

        public CustomersController(ICustomerRepository customerRepository, CustomHttpClient customHttpClient)
        {
            _customerRepository = customerRepository;
            _customHttpClient = customHttpClient;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            using (HttpClient httpClient = _customHttpClient.CreateHttpClient())
            {
                return Ok(await _customerRepository.GetCustomers());
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer updatecustomer)
        {
            try
            {
                await _customerRepository.UpdateCustomer(id, updatecustomer);
                return Ok(updatecustomer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return BadRequest("Id mismatch");
            }
           
            //if (customer != null)
            //{
            //    customer.FirstName = updatecustomer.FirstName;
            //    customer.LastName = updatecustomer.LastName;
            //    customer.Email = updatecustomer.Email;        
            //    await _customerRepository.SaveChangesAsync();
            //    return Ok(customer);
            //}
            //return NotFound();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            await _customerRepository.AddCustomer(customer);
            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerRepository.DeleteCustomer(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            //var customer = await _dbContext.Customers.FindAsync(id);
            //if (customer != null)
            //{
            //    _dbContext.Remove(customer);
            //    _dbContext.SaveChanges();
            //    return Ok(customer);
            //}
            //return NotFound();
        }

        //private bool CustomerExists(int id)
        //{
        //    return _customerRepository.Customers.Any(e => e.Id == id);
        //}
    }

}

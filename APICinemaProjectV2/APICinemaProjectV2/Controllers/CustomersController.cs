﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APICinemaProject2.DAL.Database.Models;
using APICinemaProject2.DAL.Models;
using APICinemaProjectV2.DAL.Repositories;

namespace APICinemaProjectV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository context;

        public CustomersController(ICustomerRepository _context)
        {
            context = _context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                List<Customer> result = await context.GetAllCustomers(); // Ok kan typecast 99% af alt kode whoo!
                if (result == null)
                {
                    return StatusCode(500);
                }

                if (result.Count == 0)
                {
                    return NoContent();
                }

                else
                {
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return (ActionResult)StatusCode(500, ex);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            try
            {
                var customer = context.GetCustomerByID(id);

                if (customer == null)
                {
                    return NotFound();
                }

                return await customer;
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            try
            {
                if (id != customer.CustomerID)
                    return BadRequest("ID Mismatch");

                var customerToUpdate = await context.GetCustomerByID(id);

                if (customerToUpdate == null)
                {
                    return NotFound($"Customer with ID = {id} not found");
                }

                var result = await context.UpdateCustomer(customer);

                if (result != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return BadRequest("Null i Repo");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            try
            {
                await context.CreateCustomer(customer);

                return customer;
            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            try
            {
                var response = await context.DeleteCustomerByID(id);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }

            }
            catch (Exception ex)
            {
                return (ActionResult)BadRequest(ex.Message);
            }
        }
    }
}
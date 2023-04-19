using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Domain.Interfaces;
using CustomerAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.IdentityModel.Tokens;
using CustomerAPI.Application.Models;
using CustomerAPI.Service.Validators;

namespace CustomerAPI.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private IBaseService<Customer> _baseCustomerService;

        public CustomerController(IBaseService<Customer> baseCustomerService)
        {
            _baseCustomerService = baseCustomerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseCustomerService.Get<Customer>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Execute(() => _baseCustomerService.GetById<Customer>(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerModel customer)
        {
            if (customer == null)
                return NotFound();

            return Execute(() => _baseCustomerService.Add<CreateCustomerModel, CustomerModel, CustomerValidator>(customer));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UpdateCustomerModel customer)
        {
            if (customer == null)
                return NotFound();

            return Execute(() => _baseCustomerService.Update<UpdateCustomerModel, CustomerModel, CustomerValidator>(customer));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseCustomerService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

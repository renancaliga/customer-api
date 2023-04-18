using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Domain.Interfaces;
using CustomerAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CustomerAPI.Application.Controllers
{
    [Route("api/[controller]")]
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
            return Execute(() => _baseCustomerService.Get());
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

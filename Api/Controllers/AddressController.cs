using ECommerce.Application;
using ECommerce.Application.Dtos;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/Address")]
    public class AddressController : ControllerBase
    {
        private readonly ServiceInterfaces.IAddressServices _addressService;
        private readonly MyDbContext _context;
        public AddressController(ServiceInterfaces.IAddressServices addressService, MyDbContext context)
        {
            _addressService = addressService;
            _context = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAddress(CreateAddressRequestDto createAddressRequestDto)
        {
            var result = await _addressService.AddAddress(createAddressRequestDto);
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAddress()
        {
            var result = await _addressService.GetAddress();
            return Ok(result);
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateRequestDto updateRequestDto)
        {
            var result = await _addressService.UpdateAddress(updateRequestDto);
            return Ok(result);
        }
    }
}

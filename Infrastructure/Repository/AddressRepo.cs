using ECommerce.Application;
using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.Repository
{
    public class AddressRepo : RepositoryInterfaces.IAddressRepo
    {
        private readonly MyDbContext _context;
        private readonly ServiceInterfaces.IUserInfo _userInfo;

        public AddressRepo(MyDbContext context, ServiceInterfaces.IUserInfo userInfo)
        {
            _context = context;
            _userInfo = userInfo;
        }
        
        public async Task<Address> AddAddress(Address address)
        {
             _context.Address.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        public async Task<List<Address>> GetAddress()
        {
            var result = _context.Address.Where(a => a.UserId == _userInfo.UserId).ToList();
            return result;
        }
        public async Task<Address> UpdateAddress(Address address)
        {
            _context.Address.Update(address);
            await _context.SaveChangesAsync();
            return address;
        }

    }
}

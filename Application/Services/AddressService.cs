using AutoMapper;
using ECommerce.Application.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Enums;
using ECommerce.Helper;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Application.Services
{
    public class AddressService : BaseService, ServiceInterfaces.IAddressServices
    {
        private readonly MyDbContext _context;
        private readonly ServiceInterfaces.IUserInfo _userInfo;
        private readonly RepositoryInterfaces.IAddressRepo _addressRepo;
        private readonly IMapper _mapper;
        public AddressService(MyDbContext context, ServiceInterfaces.IUserInfo userInfo, RepositoryInterfaces.IAddressRepo addressRepo, IMapper mapper)
        {
            _context = context;
            _userInfo = userInfo;
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        public async Task<ResponseHandler<AddressStatus, AddressDto>> AddAddress(CreateAddressRequestDto createAddressRequestDto)
        {
            if (string.IsNullOrWhiteSpace(createAddressRequestDto.Street) ||
              string.IsNullOrWhiteSpace(createAddressRequestDto.City) ||
              string.IsNullOrWhiteSpace(createAddressRequestDto.State) ||
              string.IsNullOrWhiteSpace(createAddressRequestDto.Country) ||
              string.IsNullOrWhiteSpace(createAddressRequestDto.ZipCode))
            {
                return BuildResponse<AddressStatus, AddressDto> ("All address fields (Street, City, State, Country, ZipCode) are required.", AddressStatus.Error, 400);
            }
            var responseHandler = new ResponseHandler<AddressStatus, AddressDto>();
            var address = new Address
            {
                City = createAddressRequestDto.City,
                Country = createAddressRequestDto.Country,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsDefault = createAddressRequestDto.IsDefault,
                State = createAddressRequestDto.State,
                Street = createAddressRequestDto.Street,
                Type = createAddressRequestDto.Type,
                UserId = _userInfo.UserId,
                ZipCode = createAddressRequestDto.ZipCode,
            };
            var result = await _addressRepo.AddAddress(address);
            var addressMapper = _mapper.Map<AddressDto>(result); 
            return BuildResponse<AddressStatus, AddressDto>("Success", AddressStatus.Success, 200, addressMapper);
        }

        public async Task<ResponseHandler<AddressStatus, List<AddressDto>>> GetAddress()
        {
            var result = await _addressRepo.GetAddress();
            var mappedAddressList = _mapper.Map<List<AddressDto>>(result);
            return BuildResponse<AddressStatus, List<AddressDto>>("Success", AddressStatus.Success, 200, mappedAddressList);
        }
        public async Task<ResponseHandler<AddressStatus, AddressDto>> UpdateAddress(UpdateRequestDto updateRequestDto)
        {
            var addressData = await _context.Address.FindAsync(updateRequestDto.Id);
            if(addressData == null)
            {
                return BuildResponse<AddressStatus, AddressDto>("Address doesnt exists", AddressStatus.Error, 400);
            }
            addressData.Street = !string.IsNullOrWhiteSpace(updateRequestDto.Street)
                        ? updateRequestDto.Street
                        : addressData.Street;
            addressData.City = !string.IsNullOrWhiteSpace(updateRequestDto.City)
                                   ? updateRequestDto.City
                                   : addressData.City;
            addressData.State = !string.IsNullOrWhiteSpace(updateRequestDto.State)
                                   ? updateRequestDto.State
                                   : addressData.State;
            addressData.Country = !string.IsNullOrWhiteSpace(updateRequestDto.Country)
                                   ? updateRequestDto.Country
                                   : addressData.Country;
            addressData.ZipCode = !string.IsNullOrWhiteSpace(updateRequestDto.ZipCode)
                                   ? updateRequestDto.ZipCode
                                   : addressData.ZipCode;
            addressData.Type = updateRequestDto.Type ?? addressData.Type;
            addressData.IsDefault = updateRequestDto.IsDefault ?? addressData.IsDefault;

            addressData.UpdatedAt = DateTime.UtcNow;
            var result = await _addressRepo.UpdateAddress(addressData);
            var mappedResult = _mapper.Map<AddressDto>(result);
            return BuildResponse<AddressStatus, AddressDto>("Success", AddressStatus.Success, 200, mappedResult);

        }
    }
}



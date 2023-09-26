using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class CustomerExtensions
{
    public static CustomerDto AsDto(this Customer customer)
        => new CustomerDto
        {
            Id = customer.Id,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt,
            Email = customer.Email,
            Password = customer.Password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            WalletId = customer.WalletId,
            Wallet = customer?.Wallet?.AsDto(),
            Orders = customer?.Orders?.AsDtoList()
        };
    
    public static Customer AsEntity(this CustomerDto customerDto)
        => new Customer(
            id: customerDto.Id,
            walletId: customerDto.WalletId,
            firstName: customerDto.FirstName,
            lastName: customerDto.LastName,
            email: customerDto.Email,
            password: customerDto.Password,
            createdAt: customerDto.CreatedAt,
            updatedAt: customerDto.UpdatedAt,
            wallet: customerDto?.Wallet?.AsEntity(),
            orders: customerDto?.Orders?.AsEntityList()
        );

    public static IList<Customer> AsEntityList(this IList<CustomerDto> customerDtos)
        => customerDtos.Select(customer => customer.AsEntity()).ToList();
    
    public static IList<CustomerDto> AsDtoList(this IList<Customer> customers)
        => customers.Select(customer => customer.AsDto()).ToList();
}
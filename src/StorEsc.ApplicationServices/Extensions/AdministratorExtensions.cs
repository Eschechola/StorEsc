using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class AdministratorExtensions
{
    public static AdministratorDto AsDto(this Administrator administrator)
        => new AdministratorDto
        {
            Id = administrator.Id,
            CreatedAt = administrator.CreatedAt,
            UpdatedAt = administrator.UpdatedAt,
            Email = administrator.Email,
            Password = administrator.Password,
            FirstName = administrator.FirstName,
            LastName = administrator.LastName,
            CreatedBy = administrator.CreatedBy,
            IsEnabled = administrator.IsEnabled
        };
    
    public static Administrator AsEntity(this AdministratorDto administratorDto)
        => new Administrator(
            id: administratorDto.Id,
            firstName: administratorDto.FirstName,
            lastName: administratorDto.LastName,
            email: administratorDto.Email,
            password: administratorDto.Password,
            createdBy: administratorDto.CreatedBy,
            isEnabled: administratorDto.IsEnabled,
            createdAt: administratorDto.CreatedAt,
            updatedAt: administratorDto.UpdatedAt
        );

    public static IList<Administrator> AsEntityList(this IList<AdministratorDto> administratorDtos)
        => administratorDtos.Select(administrator => administrator.AsEntity()).ToList();
    
    public static IList<AdministratorDto> AsDtoList(this IList<Administrator> administrators)
        => administrators.Select(administrator => administrator.AsDto()).ToList();
}
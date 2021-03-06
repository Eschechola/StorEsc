using AutoMapper;
using StorEsc.Application.DTOs;
using StorEsc.Domain.Entities;

namespace StorEsc.IoC.Dependencies.Mapper.Profiles;

public class VoucherProfile : Profile
{
    public VoucherProfile()
    {
        CreateMap<Voucher, VoucherDTO>()
            .ReverseMap();
    }
}
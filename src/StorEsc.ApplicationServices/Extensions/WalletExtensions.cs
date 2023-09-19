using StorEsc.Application.Dtos;
using StorEsc.Domain.Entities;

namespace StorEsc.Application.Extensions;

public static class WalletExtensions
{
    public static WalletDto AsDto(this Wallet wallet)
        => new WalletDto
        {
            Id = wallet.Id,
            Amount = wallet.Amount,
            UpdatedAt = wallet.UpdatedAt,
            CreatedAt = wallet.CreatedAt
        };
    
    public static Wallet AsEntity(this WalletDto walletDto)
        => new Wallet(
            id: walletDto.Id,
            amount: walletDto.Amount,
            updatedAt: walletDto.UpdatedAt,
            createdAt: walletDto.CreatedAt
        );

    public static IList<Wallet> AsEntityList(this IList<WalletDto> walletDtos)
        => walletDtos.Select(wallet => wallet.AsEntity()).ToList();
    
    public static IList<WalletDto> AsDtoList(this IList<Wallet> wallets)
        => wallets.Select(wallet => wallet.AsDto()).ToList();
}
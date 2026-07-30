using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string baskedID, CancellationToken ct = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto, TimeSpan? TLV = default, CancellationToken ct = default);
        Task<Result<bool>> DeleteBasketAsync(string basketID, CancellationToken ct = default);
    }
}
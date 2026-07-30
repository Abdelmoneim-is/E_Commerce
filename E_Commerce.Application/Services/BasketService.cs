using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Baskets;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto, TimeSpan? TLV = null, CancellationToken ct = default)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basketDto);
            var basketResult =await _basketRepository.CreateOrUpdateBasketAsync(customerBasket, TLV, ct);
            return basketResult == null ? Result<BasketDto>.Fail(Error.Failure("Basket Create Failure ", "Can Not Create Or Update Basket"))
                : Result<BasketDto>.Ok(basketDto);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketID, CancellationToken ct = default)
        {
            var result = await _basketRepository.DeleteBasketAsync(basketID, ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("BasketDelete.Failure", "Can Not Delete Basket"));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string baskedID, CancellationToken ct = default)
        {
            var result =await _basketRepository.GetBasketAsync(baskedID, ct);
            return result == null ? Result<BasketDto>.Fail(Error.NotFound("Basket Not Found")) : _mapper.Map<BasketDto>(result);
        }
    }
}

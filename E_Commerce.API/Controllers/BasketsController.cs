using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{

    public class BasketsController : APIBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> GetBasket (string id , CancellationToken ct)
        {
            var result = await _basketService.GetBasketAsync(id, ct);
            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket (BasketDto basketDto , CancellationToken ct)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basketDto, ct: ct);
            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket (string id , CancellationToken ct)
        {
            var result =await _basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}

using E_Commerce.API.Attributes;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : APIBaseController
    {
        private readonly IproductService _productService;

        public ProductsController(IproductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        [RedisCash(90)]
        public async Task<ActionResult<PaginatedResult<ProductsDto>>> GetAllProducts(
            [FromQuery] ProductQueryParams queryParams,
            CancellationToken ct)
        {
            var result = await _productService.GetAllProductsAsync(queryParams, ct);
            return ToActionResult(result);
        }

        //Get Product By Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductsDto>> GetProductById (int id , CancellationToken ct)
        {
            var result = await _productService.GetByIdAsync (id , ct);
            return ToActionResult(result);
        }
        //Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes (CancellationToken ct)
        {
            var result = await _productService.GetAllTypestsAsync (ct);
            return ToActionResult(result);
        }
        //Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands (CancellationToken ct)
        {
            var result = await _productService.GetAllBrandsAsync (ct);
            return ToActionResult(result);
        }

    }
}

using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IproductService
    {
        Task<Result<PaginatedResult<ProductsDto>>> GetAllProductsAsync(ProductQueryParams queryParams,CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypestsAsync(CancellationToken ct = default);
        Task<Result<ProductsDto>> GetByIdAsync(int id, CancellationToken ct = default);


    }
}

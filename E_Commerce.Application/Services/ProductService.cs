using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOS.Products;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class ProductService : IproductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands =await _unitOfWork.GetGenericRepository<ProductBrand , int>().GetAllAsync(ct);
            var mapper = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(mapper);
        }

        public async Task<Result<PaginatedResult <ProductsDto>>> GetAllProductsAsync(ProductQueryParams queryParams , CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(queryParams);
            var products =await _unitOfWork.GetGenericRepository<Product , int>().GetAllAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductsDto>>(products);
            var ProductCounts = new ProductCountSpecifications(queryParams);
            var countAllProducts = await _unitOfWork.GetGenericRepository<Product, int>().CountElementAsync(ProductCounts);
            var result = new PaginatedResult<ProductsDto>(queryParams.PageSize, queryParams.PageIndex, countAllProducts, data);
            return Result<PaginatedResult <ProductsDto>>.Ok(result);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypestsAsync(CancellationToken ct = default)
        {
            var types =await _unitOfWork.GetGenericRepository<ProductType , int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<TypeDto>>(types);
            return Result<IReadOnlyList<TypeDto>>.Ok(data);
        }

        public async Task<Result<ProductsDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product =await _unitOfWork.GetGenericRepository<Product , int>().GetByIdAsync(spec, ct);
            if (product == null)
                return Result<ProductsDto>.Fail(Error.NotFound("Product Not Found", $"Product With {id} Not Found"));
            var data = _mapper.Map<ProductsDto>(product);
            return Result<ProductsDto>.Ok(data);
        }
    }
}

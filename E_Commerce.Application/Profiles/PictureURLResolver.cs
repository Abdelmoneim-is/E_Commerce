using AutoMapper;
using E_Commerce.Application.DTOS.Products;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class PictureURLResolver : IValueResolver<Product, ProductsDto, string>
    {
        private readonly URLSettings _UrlSetting;

        public PictureURLResolver(IOptions<URLSettings> options)
        {
           _UrlSetting = options.Value;
        }

        public string Resolve(Product source, ProductsDto destination, string destMember, ResolutionContext context)
        {
            var baseURl = _UrlSetting.BaseURL.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{baseURl}/Files/{path}";
        }
    }

    public class URLSettings
    {
        public string BaseURL { get; set; }
    }
}

using ShopApi.Dtos;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Mapping
{
    public class ProductMapper
    {
        public static ProductDto ProductToDto(Product entity)
        {
            var productDto = new ProductDto()
            {
                Name = entity.Name,
                Description = entity.Description,
                Slug = entity.Slug,
                Price = entity.Price,
                Quantity = entity.Quantity

            };
            return productDto;
        }

        public static Product ProductDtoToEntity(ProductDto dto)
        {
            var product = new Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Slug = dto.Slug,
                Price = dto.Price,
                Quantity = dto.Quantity

            };
            return product;
        }
    }
}

using ShopApi.Context;
using ShopApi.Dtos;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Mapping
{
    public class OrderMapper
    {
        private readonly ShopContext context;
        public OrderMapper(ShopContext context) {
            this.context = context;
        }

        public static OrderDto OrderToDto(Order entity)
        {
            var orderDto = new OrderDto()
            {
                Date = entity.Date,

                Quantity = entity.Quantity,
                Status = entity.Status,

            };
            return orderDto;
        }
        public  static  Order OrderDtoToEntity(OrderDto dto)
        {
            var order = new Order()
            {
                Date = dto.Date,
                Status = dto.Status,
                Quantity = dto.Quantity,
                ProductId =dto.ProductId,
                UserId = dto.UserId,
            };
           

            return order;
        }
    }
}

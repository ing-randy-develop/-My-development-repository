using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Dtos
{
    public class OrderDto
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}

using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Slug { get; set; }

        public double Price { get; set; }

        public int? IdOrden { get; set; }

    }
}

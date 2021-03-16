using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Slug { get; set; }

        [Required]
        public double Price { get; set; }

    }
}

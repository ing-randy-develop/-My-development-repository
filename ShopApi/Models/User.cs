using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Models
{
    public class User:IdentityUser
    {
        [Required]
        [PersonalData]
        public string Name { get; set; }
        [Required]
        [PersonalData]
        public string Lastname { get; set; }
      
        public List<Order> Orders { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Context
{
    public class ShopContext:IdentityDbContext<User>
    {
        private readonly DbContextOptions _options;
        public ShopContext(DbContextOptions<ShopContext> options)
           : base(options)
        {
            _options = options;
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminId = "e070b9df-db19-4c0a-8952-15ef575065b9";
            string sellerId = "bab0bc8e-413c-4b9f-9110-63c8e8f91cdf";
            string clientId = "2c614452-0ff9-433d-8e43-321d3af7c3fe";

            string adminRoleId = "72045e5d-20bd-4c29-a1de-b07e8561cf7f";
            string sellerRoleId = "6ed73156-f3ce-40e3-a419-a8988614e5b2";
            string clientRoleId = "c0970c59-e93c-4e94-91d7-0aec5d1acf00";

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
            });

            

            var admin = new User
            {
                Id = adminId,
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Name = "NombreAdmin",
                Lastname = "ApellidoAdmin",
                UserName = "admin",
                NormalizedUserName = "ADMIN"
            };

            var seller = new User
            {
                Id = sellerId,
                Email = "vendedor@gmail.com",
                NormalizedEmail = "VENDEDOR@GMAIL.COM",
                Name = "NombreVendedor",
                Lastname = "ApellidoVendedor",
                UserName = "vendedor",
                NormalizedUserName = "VENDEDOR"

            };
            var client = new User
            {
                Id = clientId,
                Email = "cliente@gmail.com",
                NormalizedEmail = "CLIENTE@GMAIL.COM",
                Name = "NombreCliente",
                Lastname = "ApellidoCliente",
                UserName = "cliente",
                NormalizedUserName = "CLIENTE"

            };

            PasswordHasher<User> passAdmin = new PasswordHasher<User>();
            admin.PasswordHash = passAdmin.HashPassword(admin, "PassAdmin123");

            PasswordHasher<User> passSeller = new PasswordHasher<User>();
            seller.PasswordHash = passSeller.HashPassword(seller, "PassVendedor123");

            PasswordHasher<User> passClient = new PasswordHasher<User>();
            client.PasswordHash = passClient.HashPassword(client, "PassCliente123");

            modelBuilder.Entity<User>().HasData(admin, seller, client);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name="administrador", NormalizedName= "ADMINISTRADOR", Id = adminRoleId, ConcurrencyStamp = adminRoleId }, 
                new IdentityRole { Name= "vendedor", NormalizedName= "VENDEDOR", Id = sellerRoleId, ConcurrencyStamp = sellerRoleId }, 
                new IdentityRole { Name= "cliente", NormalizedName= "CLIENTE", Id = clientRoleId, ConcurrencyStamp = clientRoleId } 
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            },
            new IdentityUserRole<string>
            {
                RoleId = sellerRoleId,
                UserId = sellerId
            },
            new IdentityUserRole<string>
            {
                RoleId = clientRoleId,
                UserId = clientId
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

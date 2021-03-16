using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Dtos;
using ShopApi.Mapping;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext context;
        public ProductController(ShopContext context)
        {
            this.context = context;
        }

        // GET: ProductController
        [Authorize(Roles = "cliente, vendedor,administrador")]
        [HttpGet(Name ="GetAllProduct")]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Product.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "cliente")]
        [HttpPost]
        [Route("BuyProduct")]
        public ActionResult PostComprarProducto(OrderDto order)
        {
            try
            {
                var newProduct = context.Product.FirstOrDefault(data => data.ProductId == order.ProductId);
                if (newProduct.Quantity >= order.Quantity)
                {
                    newProduct.Quantity = newProduct.Quantity - order.Quantity;
                    context.Entry(newProduct).State = EntityState.Modified;
                    var newOrder = OrderMapper.OrderDtoToEntity(order);
                    context.Order.Add(newOrder);
                    context.SaveChanges();
                    return Ok(newOrder);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // GET by id: ProductController
        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult Get(int id)
        {
            try
            {
                var product = context.Product.FirstOrDefault(p => p.ProductId == id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: ProductController/Create
        [Authorize(Roles = "vendedor,administrador")]
        [HttpPost]
        public IActionResult Post(ProductDto productDto)
        {
            try
            {
                Product product = ProductMapper.ProductDtoToEntity(productDto);
                context.Product.Add(product);
                context.SaveChanges();
                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // POST: ProductController/Edit/5
        [Authorize(Roles = "vendedor,administrador")]
        [HttpPut("id")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            try
            {
                if (id == product.ProductId)
                {
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // POST: ProductController/Delete/5
        [Authorize(Roles = "vendedor,administrador")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = context.Product.FirstOrDefault(p => p.ProductId == id);
                if (product != null)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                    return Ok(id);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

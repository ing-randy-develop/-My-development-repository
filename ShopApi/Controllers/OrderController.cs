using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Dtos;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ShopContext _context;

        public OrderController(ShopContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "administrador")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, string status)
        {
            try
            {
                var order = _context.Order.FirstOrDefault(data => data.OrderId == id);
                if (order != null && (status.Equals(StatusDto.confirmed.ToString()) || status.Equals(StatusDto.canceled.ToString()) || status.Equals(StatusDto.created.ToString())))
                {
                    order.Status = status;
                    _context.Entry(order).State = EntityState.Modified;

                    _context.SaveChanges();
                    return Ok(order);
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

        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var order = _context.Order.FirstOrDefault(data => data.OrderId == id);
                if (order != null && order.Status.Equals(StatusDto.created.ToString()))
                {
                    _context.Order.Remove(order);
                    _context.SaveChanges();
                    return Ok(id);
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

    }
}

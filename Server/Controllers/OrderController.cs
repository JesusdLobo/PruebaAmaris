using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PruebaAmaris.Server.Repository;
using PruebaAmaris.Shared.Class;
using System.Collections.Generic;
using System.Data.SqlClient;
using static PruebaAmaris.Client.Shared.MainLayout;

namespace PruebaAmaris.Server.Controllers
{
  

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _ordenrepo;
        public OrderController(IOrder ordenrepo)
        {
            _ordenrepo = ordenrepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                var companies = await _ordenrepo.GetOrder();
                return Ok(companies);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "OrderById")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var company = await _ordenrepo.GetOrder(id);
                if (company == null)
                    return NotFound();
                return Ok(company);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Createorder(OrderDTO order)
        {
            try
            {
                var createdorder = await _ordenrepo.CreateOrder(order);
                return CreatedAtRoute("OrderById", new { id = createdorder.OrderID }, createdorder);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updateorder(int id, OrderDTO order)
        {
            try
            {
                var dborder = await _ordenrepo.GetOrder(id);
                if (dborder == null)
                    return NotFound();
                await _ordenrepo.UpdateOrder(id, order);
                return Ok();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteorder(int id)
        {
            try
            {
                var dborder = await _ordenrepo.GetOrder(id);
                if (dborder == null)
                    return NotFound();
                await _ordenrepo.DeleteOrder(id);
                return Ok();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("bulkinsert")]
        public IActionResult BulkInsertOrders(List<OrderData> orders)
        {
            _ordenrepo.BulkInsertOrders(orders);
            return Ok();
        }


    }

}

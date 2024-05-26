using Microsoft.AspNetCore.Mvc;
using SimpleApiService.Models;

namespace SimpleApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private static readonly List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, ProductId = 1, Quantity = 2 },
            new Order { Id = 2, ProductId = 2, Quantity = 1 }
        };

        [HttpGet]
        public IEnumerable<Order> Get() => Orders;

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            order.Id = Orders.Max(o => o.Id) + 1;
            Orders.Add(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}

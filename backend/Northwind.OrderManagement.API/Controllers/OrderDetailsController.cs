using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.OrderManagement.Application.Features.OrderDetails.Commands.CreateOrderDetail;
using Northwind.OrderManagement.Application.Features.OrderDetails.Commands.DeleteOrderDetail;
using Northwind.OrderManagement.Application.Features.OrderDetails.Commands.UpdateOrderDetail;
using Northwind.OrderManagement.Application.Features.OrderDetails.Queries.GetAllOrderDetails;
using Northwind.OrderManagement.Application.Features.OrderDetails.Queries.GetOrderDetailById;
using Northwind.OrderManagement.Application.Features.OrderDetails.Queries.AllOrderDetailById;

namespace Northwind.OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/orderdetails
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDetailCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // GET: api/orderdetails
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrderDetailsQuery());
            return Ok(result);
        }

        // GET: api/orderdetails/{orderId}/{productId}
        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetById(int orderId, int productId)
        {
            var query = new GetOrderDetailByIdQuery { OrderId = orderId, ProductId = productId };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        // GET: api/orderdetails/order/{orderId}
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var query = new GetAllOrderDetailByIdQuery(orderId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // DELETE: api/orderdetails/{orderId}/{productId}
        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var command = new DeleteOrderDetailCommand { OrderId = orderId, ProductId = productId };
            var response = await _mediator.Send(command);

            if (response.Message.Contains("not found"))
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        // PATCH: api/orderdetails/{orderId}/{productId}
        [HttpPatch("{orderId}/{productId}")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDetailCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Message.Contains("not found"))
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}

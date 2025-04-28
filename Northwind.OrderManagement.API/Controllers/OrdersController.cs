using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using Northwind.OrderManagement.Application.Features.Orders.Queries.Northwind.OrderManagement.Application.Features.Orders.Queries.GetOrderById;
using Northwind.OrderManagement.Application.Features.Orders.Commands.DeteleOrder;
using Northwind.OrderManagement.Application.Exceptions;
using Northwind.OrderManagement.Application.Features.Orders.Commands.UpdateOrder;

namespace Northwind.OrderManagement.API.Controllers
{
    namespace Northwind.OrderManagement.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OrdersController : ControllerBase
        {
            private readonly IMediator _mediator;

            public OrdersController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
            {
                if (command == null)
                    return BadRequest();

                var orderId = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
            }


            //GET: api/orders/
            [HttpGet]
            public async Task<IActionResult> GetAllOrders()
            {
                var orders = await _mediator.Send(new GetAllOrdersQuery());
                return Ok(orders);
            }

            //GET: api/orders/{id}
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var query = new GetOrderByIdQuery(id);
                var order = await _mediator.Send(query);

                if (order == null)
                    return NotFound();

                return Ok(order);
            }

            // DELETE: api/orders/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var command = new DeleteOrderCommand { OrderId = id };
                var response = await _mediator.Send(command);

                // Devolver la respuesta con el mensaje de confirmación
                return Ok(response);
            }

            // PUT: api/orders/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderCommand command)
            {
                if (id != command.OrderId)
                {
                    return BadRequest("Order ID mismatch.");
                }

                var response = await _mediator.Send(command);

                if (response.Message.Contains("not found"))
                {
                    return NotFound(response);
                }

                return Ok(response);
            }


        }
    }
}

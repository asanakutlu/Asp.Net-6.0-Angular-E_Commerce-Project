using E_CommerceAPI.Application.Consts;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Features.Commands.Order.CompletedOrder;
using E_CommerceAPI.Application.Features.Commands.Order.CreateOrder;
using E_CommerceAPI.Application.Features.Queries.Order.GetAllOrders;
using E_CommerceAPI.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes="Admin")]
    public class OrdersController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Writing, Definition = "Create order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
           CreateOrderCommandResponse response= await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Reading, Definition = "Get Order")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Reading, Definition = "Get Id Order")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdRequest)
        {
            GetOrderByIdQueryResponse response = await _mediator.Send(getOrderByIdRequest);
            return Ok(response);
        }
        [HttpGet("completed-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = Application.Enums.ActionType.Updating, Definition = "Completed order")]
        public async Task<IActionResult> CompletedOrder([FromRoute] CompletedOrderCommandRequest completedOrderCommandRequest)
        {
            CompletedOrderCommandResponse response=await _mediator.Send(completedOrderCommandRequest);
            return Ok(response);
        }
    }    
}

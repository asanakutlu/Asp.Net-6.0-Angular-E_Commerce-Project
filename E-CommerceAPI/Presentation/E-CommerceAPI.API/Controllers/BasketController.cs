using E_CommerceAPI.Application.Consts;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Features.Commands.Basket.AddItemToBasket;
using E_CommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using E_CommerceAPI.Application.Features.Commands.Basket.UptadateQuanlity;
using E_CommerceAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class BasketController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.Baskets,ActionType =Application.Enums.ActionType.Reading,Definition ="Get Basket Items")]
        public async Task<IActionResult> GetBasketItems([FromQuery]GetBasketITemsQueryRequest getBasketITemsQueryRequest)
        {
            List<GetBasketITemsQueryResponse> response=await _mediator.Send(getBasketITemsQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = Application.Enums.ActionType.Writing, Definition = "Add Item to Basket")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = Application.Enums.ActionType.Updating, Definition = "update Quantity")]
        public async Task<IActionResult> UpdateQuatity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            UpdateQuantityCommandResponse response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{BasketItemId")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = Application.Enums.ActionType.Deleting, Definition = "Remove Basket Item")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            RemoveBasketItemCommandResponse response=await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}

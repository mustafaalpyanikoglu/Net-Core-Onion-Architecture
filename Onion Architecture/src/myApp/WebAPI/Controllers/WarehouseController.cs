using Application.Features.Warehouses.Queries.GetByIdWarehouse;
using Application.Features.Warehouses.Dtos;
using Application.Features.Warehouses.Queries.GetListWarehouse;
using Core.Application.Requests;
using Core.CrossCuttingConcerns;
using Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Application.Features.Warehouses.Commands.DeleteWarehouse;
using Application.Features.Warehouses.Commands.UpdateWarehouse;
using Application.Features.OperationClaims.Models;
using Application.Features.Warehouses.Commands.CreateWarehouse;
using Application.Features.CustomerWarehouseCosts.Commands.DeleteCustomerWarehouseCosts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : BaseController
    {
        [ProducesResponseType(typeof(CreatedWarehouseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(description: ResponseDescriptions.EXCEPTION_DETAIL)]
        [HttpPost("add")] 
        public async Task<IActionResult> Add([FromBody] CreateWarehouseCommand createWarehouseCommand)
        {
            CreatedWarehouseDto result = await Mediator.Send(createWarehouseCommand);
            return Created("", result);
        }

        [ProducesResponseType(typeof(DeletedWarehouseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(description: ResponseDescriptions.EXCEPTION_DETAIL)]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteWarehouseCommand deleteWarehouseCommand)
        {
            DeletedWarehouseDto result = await Mediator.Send(deleteWarehouseCommand);
            return Ok(result);
        }

        [ProducesResponseType(typeof(UpdatedWarehouseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(description: ResponseDescriptions.EXCEPTION_DETAIL)]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] UpdateWarehouseCommand updateWarehouseCommand)
        {
            UpdatedWarehouseDto result = await Mediator.Send(updateWarehouseCommand);
            return Ok(result);
        }

        [ProducesResponseType(typeof(WarehouseListModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(WarehouseListModel), StatusCodes.Status200OK)]
        [SwaggerOperation(description: ResponseDescriptions.EXCEPTION_DETAIL)]
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListWarehouseQuery getListWarehouseQuery = new() { PageRequest = pageRequest };
            WarehouseListModel result = await Mediator.Send(getListWarehouseQuery);
            return Ok(result);
        }

        [ProducesResponseType(typeof(WarehouseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(description: ResponseDescriptions.EXCEPTION_DETAIL)]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetByIdWarehouseQuery getByIdWarehouseQuery = new() { Id = id };
            WarehouseDto result = await Mediator.Send(getByIdWarehouseQuery);
            return Ok(result);
        }
    }
}

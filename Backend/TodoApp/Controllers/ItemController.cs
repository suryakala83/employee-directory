using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Data.Entities;
using TodoApp.Service.DTO;
using TodoApp.Service.Interfaces;

namespace TodoApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IItemService itemService) : Controller
    {
        [HttpPost]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Item>> Add(TaskItem item)
        {
            var addItem = await itemService.Add(item);
            return Ok(new Response<Item> { StatusCode = 201, Message = "Item Added Successfully", Payload = addItem, IsSuccess = true });
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Item>> Update(int Id, TaskItem updateItem)
        {
            var item = await itemService.Update(Id, updateItem);
            return Ok(new Response<Item> { StatusCode = 200, Message = $"Item with Id {Id} updated successfully", Payload = item, IsSuccess = true });
        }

        [HttpPut("completeStatus/{id}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Item>> UpdateTaskCompleteStatus(int id, bool isCompleted = false)
        {
            var item = await itemService.UpdateTaskCompleteStatus(id, isCompleted);
            return Ok(new Response<Item> { StatusCode = 200, Message = $"Item with Id {id} updated successfully", Payload = item, IsSuccess = true });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await itemService.Delete(id);
            return Ok(new Response<Item> { StatusCode = 200, Message = $"Item with Id {id} deleted successfully", IsSuccess = true });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var item = await itemService.Get(id);
            return Ok(new Response<Item> { StatusCode = 200, Message = $"Item with Id {id} retrieved successfully", Payload = item, IsSuccess = true });
        }

        [HttpGet("tasks")]
        [ProducesResponseType(typeof(List<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItems([FromQuery] TaskFilter filter = TaskFilter.All)
        {
            var items = await itemService.GetItems(filter);
            return Ok(new Response<List<Item>> { StatusCode = 200, Message = $"{items.Count} Items retrieved successfully", Payload = items, IsSuccess = true });
        }

        [HttpDelete("delete-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAll()
        {
            await itemService.DeleteAll();
            return Ok(new Response<string> { StatusCode = 200, Message = "All tasks deleted successfully", IsSuccess = true });
        }
    }
}

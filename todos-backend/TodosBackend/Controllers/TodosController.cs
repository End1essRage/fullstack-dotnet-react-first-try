using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodosBackend.CommunicationModels.DTOs;
using TodosBackend.Data.DataAccess.Abstractions;
using TodosBackend.Data.Models;
using TodosBackend.Services;
using TodosBackend.Services.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace TodosBackend.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class TodosController : ControllerBase
	{
		private ITodoRepository _repository;
        private IUserService _userService;
        public TodosController(ITodoRepository repository, IUserService userService)
		{
			_repository = repository;
			_userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Todo>>> GetSome([FromQuery] int count)
		{
			var items = await _repository.GetSomeAsync(count);
            return Ok(items);
		}

        [Route("my")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Todo>>> GetUserTodos()
        {
            var curUserId = _userService.GetCurrentUserId();
            if (curUserId < 0)
                return BadRequest("User not found");

            var items = await _repository.GetUserTodos(curUserId);

            return Ok(items);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Todo>> CreateNew(TodoDTO dto)
		{
            var curUserId = _userService.GetCurrentUserId();
            if (curUserId < 0)
                return BadRequest("User not found");

            var newTodo = new Todo() { Title = dto.Title, UserId = curUserId };

            var result = _repository.Create(newTodo);
            await _repository.SaveChangesAsync();

			return Ok(result);
		}

		[HttpPatch]
        [Authorize]
        public async Task<ActionResult> ToggleComplete(int id)
		{
			var item = await _repository.GetOneAsync(id);
			if(item == null)
			{
				return BadRequest();
			}

			await _repository.ToggleComplete(id);

            return Ok();
        }

		[HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteTodo(int id)
		{
            var item = await _repository.GetOneAsync(id);
            if (item == null)
            {
                return BadRequest();
            }

            _repository.Delete(item);
            await _repository.SaveChangesAsync();
            return Ok();
        }
    }
}
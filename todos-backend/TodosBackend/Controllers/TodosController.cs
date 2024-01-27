using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodosBackend.CommunicationModes;
using TodosBackend.Data.Abstractions;
using TodosBackend.Models;
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
        public async Task<ActionResult<List<Todo>>> GetAll()
		{
			var items = await _repository.GetAllTodos();
			return Ok(items);
		}

        [Route("my")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Todo>>> GetUserTodos()
        {
            var curUser = await _userService.GetCurrentUser();
            if (curUser == null)
                return BadRequest("User not found");

            var items = await _repository.GetUserTodos(curUser.Id);

            return Ok(items);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Todo>> CreateNew(TodoDTO dto)
		{
            var curUser = await _userService.GetCurrentUser();
            if (curUser == null)
                return BadRequest("User not found");

            var newTodo = new Todo() { Title = dto.Title, User = curUser };

            var result = await _repository.AddTodo(newTodo);

			return Ok(newTodo);
		}

		[HttpPatch]
        [Authorize]
        public async Task<ActionResult> ToggleComplete(int id)
		{
			var item = await _repository.GetTodoById(id);
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
            var item = await _repository.GetTodoById(id);
            if (item == null)
            {
                return BadRequest();
            }

            await _repository.DeleteTodo(id);

            return Ok();
        }
    }
}
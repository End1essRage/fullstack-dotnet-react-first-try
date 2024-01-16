using Microsoft.AspNetCore.Mvc;
using TodosBackend.Data;
using static System.Net.Mime.MediaTypeNames;

namespace TodosBackend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TodosController : ControllerBase
	{
		private ITodoRepository _repository;

		public TodosController(ITodoRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<ActionResult<List<Todo>>> GetAll()
		{
			var items = await _repository.GetAllTodos();
			return Ok(items);
		}

		[HttpPost]
		public async Task<ActionResult<Todo>> CreateNew(TodoDTO dto)
		{
			Todo newTodo;

			try
			{
                newTodo = await _repository.AddTodo(dto.Title);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(newTodo);
		}

		[HttpPatch]
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
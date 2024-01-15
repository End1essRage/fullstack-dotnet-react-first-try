using Microsoft.AspNetCore.Mvc;
using TodosBackend.Data;

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
			Console.WriteLine("request");
			var items = await _repository.GetAllTodos();
			return Ok(items);
		}

		[HttpPost]
		public async Task<ActionResult> CreateNew(string text)
		{
			try
			{
				await _repository.AddTodo(text);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok();
		}
	}
}
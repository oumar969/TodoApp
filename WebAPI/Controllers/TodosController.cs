using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]// vi bruger ApiController, fordi vi skal bruge http requests.
[Route("[controller]")]// vi bruger Route, fordi vi skal bruge en route til vores http requests.
public class TodosController : ControllerBase // ControllerBase er en klasse, som vi bruger til at lave vores controller.
{
    private readonly ITodoLogic todoLogic;//readonly betyder, at vi ikke kan ændre på den.

    public TodosController(ITodoLogic todoLogic)
    {
        this.todoLogic = todoLogic;
    }
    
    [HttpPost] // vi bruger HttpPost, fordi vi skal bruge en post request.
    public async Task<ActionResult<Todo>> CreateAsync([FromBody]TodoCreationDto dto) //acync betyder, at vi skal bruge en async metode. //[FromBody] betyder, at vi skal bruge en body i vores request.
    {//async er en metode, som ikke blokerer vores program, mens den kører.
        try
        {
            Todo created = await todoLogic.CreateAsync(dto);
            return Created($"/todos/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAsync([FromQuery] string? username, [FromQuery] int? userId,
        [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {// IEnumerable er en liste, som vi bruger til at gemme vores data.
        
            try
            {
                SearchTodoParametersDto parameters = new(username, userId, completedStatus, titleContains); //parameters er vores søgeparametre.
                var todos = await todoLogic.GetAsync(parameters);//todoLogic er vores logik, som vi bruger til at lave vores metoder.
                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] TodoUpdateDto dto)
    {
        try
        {
            await todoLogic.UpdateAsync(dto); //await betyder, at vi venter på, at vores metode er færdig. //todoLogic er vores logik, som vi bruger til at lave vores metoder. //updateAsync er vores metode, som vi bruger til at opdatere vores data.
            return Ok();//Ok betyder, at vi har fået en succesfuld request. 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromBody] int id)
    {
        try
        {
            await todoLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            TodoBasicDto todo = await todoLogic.GetByIdAsync(id);
            return Ok(todo);
    
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
        
    
}
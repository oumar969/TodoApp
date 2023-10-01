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
    
}
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UserController
{
    [ApiController]// vi bruger ApiController, fordi vi skal bruge http requests.
    [Route("[controller]")]// vi bruger Route, fordi vi skal bruge en route til vores http requests.
    public class UserController : ControllerBase// vi bruger ControllerBase, fordi vi skal bruge http requests.
    {
        private readonly IUserLogic _userLogic;//readonly betyder, at vi ikke kan ændre på den.

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        public async Task<AcceptedResult<User>> CreateAsync(UserCreationDto dto)
        {
            try
            {
                User user = await _userLogic.CreateAsync(dto);
                return Created($"/user/{user.Id}", user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            } 
        }
    }
}
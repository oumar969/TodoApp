using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
     Task<User> CreateAsync(UserCreationDto userCreationDto);
     public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);//IEnumerable er en liste af User
     
}
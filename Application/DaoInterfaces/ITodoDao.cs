using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface ITodoDao // Daointerfaces er interfaces til vores Daoer for at kunne bruge Dependency Injection
{
    Task<Todo> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
    Task UpdateAsync(Todo todo);

    Task<Todo> GetByIdAsync(int id);

    Task DeleteAsync(int id);

}
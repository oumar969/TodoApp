using Domain;

namespace Application.DaoInterfaces;

public interface ITodoDao // Daointerfaces er interfaces til vores Daoer for at kunne bruge Dependency Injection
{
    Task<Todo> CreateAsync(Todo todo);

}
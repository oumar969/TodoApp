using Domain;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IUSerDao // denne klasse er en interface, som vi bruger til at definere, hvad vores User Dao skal kunne.
{
    Task<User> CreateAsync(User user);// vi bruger Task, fordi vi skal bruge async.
    Task<User?> GetByUserNameAsync(string userName);// vi bruger ? fordi vi ikke er sikre på at vi får en User tilbage.
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);

}
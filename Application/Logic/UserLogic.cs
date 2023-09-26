using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUSerDao userDao;
    
    public UserLogic(IUSerDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUserNameAsync(dto.UserName);
        if (existing != null)
        
            throw new Exception("User already exists");
        

        ValidateDate(dto);
        
        User toCreate = new User
        {
            UserName = dto.UserName
        };
        
        User created = await userDao.CreateAsync(toCreate);
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public static void ValidateDate(UserCreationDto userCreationDto)// denne metode bruger vi til at validere vores data.
    {
        string userName = userCreationDto.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters long");
        if (userName.Length > 15)
            throw new Exception("Username must be at most 16 characters long");
    }
}
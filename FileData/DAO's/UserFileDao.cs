﻿using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;

namespace FileData.DAO_s;

public class UserFileDao : IUSerDao
{
    private readonly FileContext context;
    
    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        context.Users.Add(user);
            context.SaveChanges();

            return Task.FromResult(user);
        }
    

    public Task<User?> GetByUserNameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        // Hent brugeren med det givne id fra databasen
        User existing = context.Users.FirstOrDefault(u => u.Id == id);

        // Returner brugeren som en færdig opgave
        return Task.FromResult(existing);
    }
}
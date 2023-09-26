using Application.DaoInterfaces;
using Domain;

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
}
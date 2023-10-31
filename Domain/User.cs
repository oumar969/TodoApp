using System.Text.Json.Serialization;

namespace Domain;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public ICollection<Todo> Todos { get; set; }
    
}
using Domain;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; } // vi kan ellers bruge List, Ilist.
    public ICollection<Todo> Todos { get; set; }
    
}
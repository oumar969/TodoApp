using System.Text.Json;
using Domain;

namespace FileData;

public class FileContext // denne klasse er for at læse og skrive til filen.
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    public ICollection<Todo> Todos
    {
        get
        {
            LoadData();
            return dataContainer!.Todos; // ! betyder at vi er sikre på at dataContainer ikke er null.
        }
    }
    public ICollection<User> Users
    {
        get
        {
            LoadData(); 
            return dataContainer!.Users;
        }
    }
    
    private void LoadData()
    {
        if (dataContainer != null) return; 
        if (!File.Exists(filePath))
        {
            dataContainer = new()
            {
                Todos = new List<Todo>(),
                Users = new List<User>()
            };
            return;
        }

        string content = File.ReadAllText(filePath); // læser filen.
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content); // deserializer filen.
    }
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
    }

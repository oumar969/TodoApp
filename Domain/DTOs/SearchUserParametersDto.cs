namespace Domain.DTOs;

public class SearchUserParametersDto //klaseen hjælper med at søge efter brugere
{
    public string? UsernameContains { get;  } //string? betyder at den kan være null

    public SearchUserParametersDto(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
    
}
﻿using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> CreateAsync(TodoCreationDto dto);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
    
    Task UpdateAsync(TodoUpdateDto todo);
    Task DeleteAsync(int id);
    
    

}
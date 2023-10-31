﻿using Application.DaoInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class TodoEfcDao:  ITodoDao
{
    
    private readonly TodoContext context;

    public TodoEfcDao(TodoContext context)
    {
        this.context = context;
    }
    
    
        public async Task<Todo> CreateAsync(Todo todo)
        {
            EntityEntry<Todo> added = await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
            return added.Entity;        }

        public async Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
        {
            IQueryable<Todo> query = context.Todos.Include(todo => todo.Owner).AsQueryable();
    
            if (!string.IsNullOrEmpty(searchParameters.Username))
            {
                // we know username is unique, so just fetch the first
                query = query.Where(todo =>
                    todo.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
            }
    
            if (searchParameters.UserId != null)
            {
                query = query.Where(t => t.Owner.Id == searchParameters.UserId);
            }
    
            if (searchParameters.CompletedStatus != null)
            {
                query = query.Where(t => t.IsCompleted == searchParameters.CompletedStatus);
            }
    
            if (!string.IsNullOrEmpty(searchParameters.TitleContains))
            {
                query = query.Where(t =>
                    t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
            }

            List<Todo> result = await query.ToListAsync();
            return result;
        }

        public async Task UpdateAsync(Todo todo)
        {
            context.Todos.Update(todo);
            await context.SaveChangesAsync();
        }

        public async Task<Todo?> GetByIdAsync(int todoId)
        {
            Todo? found = await context.Todos
                .Include(todo => todo.Owner)
                .SingleOrDefaultAsync(todo => todo.Id == todoId);
            return found;
        }

        public async Task DeleteAsync(int id)
        {
            Todo? existing = await GetByIdAsync(id);
            if (existing == null)
            {
                throw new Exception($"Todo with id {id} not found");
            }

            context.Todos.Remove(existing);
            await context.SaveChangesAsync();
        }
    }
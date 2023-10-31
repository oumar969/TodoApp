using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using EfcDataAccess;
using FileData;
using FileData.DAO_s;
using HttpClients.ClientInterfaces;
using HttpClients.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FileContext>();

builder.Services.AddDbContext<TodoContext>();


builder.Services.AddScoped<IUSerDao, UserFileDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();



builder.Services.AddScoped<ITodoDao, TodoFileDao>();
builder.Services.AddScoped<ITodoLogic, TodoLogic>();


builder.Services.AddScoped<IUSerDao, UserFileDao>();
builder.Services.AddScoped<ITodoDao, TodoFileDao>();

builder.Services.AddScoped<IUserService, UserHttpClient>();

builder.Services.AddScoped(  
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("https://localhost:7093") 
        }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Presentation.Services;
using Data.Interfaces;
using Data.Repositories;
using Presentation.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IEventRepository, EventRepository>(); 
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEventService, EventService>();


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("VentixeDatabaseConnection")));

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();

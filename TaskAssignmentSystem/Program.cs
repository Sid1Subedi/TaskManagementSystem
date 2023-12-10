using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaskAssignmentSystem.Interfaces;
using TaskAssignmentSystem.Repos;
using TaskAssignmentSystem.TaskAssignmentDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();

    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
});

// Add DI
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();

// Add Conn String
var connectionString = builder.Configuration.GetConnectionString("TaskAssignmentSystemConnection");
builder.Services.AddDbContext<TaskAssignmentSystemDBContext>(options => options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

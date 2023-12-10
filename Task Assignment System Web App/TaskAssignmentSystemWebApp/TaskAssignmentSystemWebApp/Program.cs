using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TaskAssignmentSystemWebApp.Interfaces;
using TaskAssignmentSystemWebApp.Repos;
using TaskAssignmentSystemWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()  // This makes property names case-insensitive
    };

    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
});

builder.Services.AddScoped<WebAPICallServices>();

// Add services to the container.
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();

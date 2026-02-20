using Microsoft.EntityFrameworkCore;
using WeatherForcastApp.Web.Data;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
// get from configmap
var dbServer = builder.Configuration["MSSQL_SERVER"];
var dbPort = builder.Configuration["MSSQL_PORT"];
// get from secret
var dbPass = builder.Configuration["MSSQL_PASS"];

var conn = $"Server={dbServer},{dbPort};Initial Catalog=testdbcontext;User ID=sa Password={dbPass};MultipleActiveResultSets=True;Integrated security=False;TrustServerCertificate=True;"

Console.WriteLine($"connection string: {conn}");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(conn);
});

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var dbContext = services.GetRequiredService<AppDbContext>();
//     dbContext.Database.Migrate();
// }

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

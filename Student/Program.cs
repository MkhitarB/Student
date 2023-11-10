using Microsoft.EntityFrameworkCore;
using Student.Application.ApplicationProgram;
using Student.BLL.Mediator;
using Student.DAL;
using Student.Extensions;
using Student.Infrastructure.NewFolder;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("AppSettings").GetRequiredSection("ConnectionString").Value;
ConstValues.ConnectionString = connectionString;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Token
builder.Services.ConfigureJwtToken(builder.Environment);

builder.Services.RegisterComponents();
ProgramHelper.IsServerConnected();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.Load("Student.BLL")));

// Configure DbContext
builder.Services.AddDbContext<EntityDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});



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

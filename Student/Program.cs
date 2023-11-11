using Microsoft.EntityFrameworkCore;
using Student.API.Extensions;
using Student.Application.ApplicationProgram;
using Student.Application.Seeds;
using Student.DAL;
using Student.Extensions;
using Student.Infrastructure;
using Student.Infrastructure.NewFolder;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("AppSettings").GetRequiredSection("ConnectionString").Value;
ConstValues.ConnectionString = connectionString;
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Token
builder.Services.ConfigureJwtToken(builder.Environment);
builder.Services.ConfigureSwagger();
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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EntityDbContext>();
    db.Database.EnsureCreated();

    DbSeeder.SeedAsync(db).Wait();
}


app.Run();

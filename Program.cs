using FluentValidation.AspNetCore;
using Serilog;
using TakeHomeAssignment.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Set minimum logging level
    .WriteTo.Console() // Log to console
    .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day) // Log to file
    .CreateLogger();

// Add services to the container.
builder.Host.UseSerilog(); // Use Serilog for logging

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PeopleValidator>());
builder.Services.AddScoped<IPeopleService, PeopleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

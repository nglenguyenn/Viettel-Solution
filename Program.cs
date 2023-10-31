using Microsoft.EntityFrameworkCore;
using Viettel_Solution.Models;
using Viettel_Solution.Storage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IStorageService, FileStorageService>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add services for Sql Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ViettelSolutionDbConext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();

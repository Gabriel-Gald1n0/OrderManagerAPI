using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Mvc;

using OrderManagerAPI.Services.Product;
using OrderManagerAPI.Services.User;
using OrderManagerAPI.Services.Request;
using OrderManagerAPI.Services.TypesProduct;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers() 
    .ConfigureApiBehaviorOptions(options =>
    {   // limita a resposta de error 400
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray() ?? Array.Empty<string>()
                );

            var result = new
            {
                status = 400,
                errors = errors
            };

            return new BadRequestObjectResult(result);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductInterface, ProductService>();
builder.Services.AddScoped<UserInterface, UserService>();
builder.Services.AddScoped<RequestInterface, RequestService>();
builder.Services.AddScoped<TypesProductInterface, TypesProductService>();


// Configure MySQL connection string
string? mySqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext with MySQL
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

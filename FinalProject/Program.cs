using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Register the AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register and configure NSwag
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "FinalProject API";
    config.Version = "v1";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Enable and configure NSwag middleware
app.UseOpenApi(); // Updated method name
app.UseSwaggerUi3(); // Updated method name

app.MapRazorPages();
app.MapControllers();

app.Run();

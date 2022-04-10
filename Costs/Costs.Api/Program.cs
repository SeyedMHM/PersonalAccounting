using Costs.Api;
using Costs.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceRegistery();

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

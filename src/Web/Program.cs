using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

var AllowOrigins = "AllowOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: AllowOrigins,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var configuration = builder.Configuration;

// Add application services.
builder.Services.AddApplication();

builder.Services.AddInfrastructure();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseCors(AllowOrigins);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

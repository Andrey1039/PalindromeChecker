using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

int limit = 0;

do
{
    Console.Write("Enter max number of requests (N): ");
    int.TryParse(Console.ReadLine(), out limit);
}
while (limit <= 0 ? true : false);

builder.Services.AddRateLimiter(options =>
    options.AddConcurrencyLimiter("LimitConnections", (options) =>
    {
        options.PermitLimit = limit;
        options.QueueLimit = 0;
    }));

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

app.UseRateLimiter();

app.Run();

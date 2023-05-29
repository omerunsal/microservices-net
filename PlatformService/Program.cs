using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

if (builder.Environment.IsProduction())
{
     System.Console.WriteLine("Using SqlServer Db");
     builder.Services.AddDbContext<AppDbContext>(opt=>
     opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));

}else
{
    System.Console.WriteLine("Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"--> CommandService Endpoint {builder.Configuration["CommandService"]}");

var app = builder.Build();

 PrepDb.PrepPopulation(app, app.Environment.IsProduction());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

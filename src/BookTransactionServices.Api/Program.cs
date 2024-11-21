using BookTransactionServices.Repository.Implementation;
using Service.Implementation;
using Service.Interface;
using BookTransactionServices.Repository.Interface;
using System;
using AutoMapper;
using Service.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<PgSqlConnectionFactory>();
//builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddScoped<IPgSqlConnectionFactory, PgSqlConnectionFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperconfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperconfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//DIJ Repository
builder.Services.AddScoped<IBookTransactionRepository, BookTransactionRepository>();
//Services
builder.Services.AddScoped<IBookTransactionService, BookTransactionService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()  // Allow requests from any origin
               .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
               .AllowAnyHeader(); // Allow any HTTP headers
    });
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 8080); // Bind to port 8080
});
var app = builder.Build();
app.UseCors(); ;
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => {
           s.SwaggerEndpoint("/swagger/v1/swagger.json", "BookTransactionService");
            s.RoutePrefix = string.Empty;
        });
 }


app.UseAuthorization();

//app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


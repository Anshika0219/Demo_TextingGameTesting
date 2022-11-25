using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Persistence;
using Service.Interface;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddNewtonsoftJson
    (options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson
    (options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
var connectionString = builder.Configuration.GetConnectionString("DbCon");
builder.Services.AddDbContext<DbTextingGameContext>(option =>
option.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IUserServices, UserServices>();  //registering dependency
builder.Services.AddScoped<IEncryptServices, EncryptServices>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

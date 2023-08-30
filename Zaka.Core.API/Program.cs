using Microsoft.EntityFrameworkCore;
using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Application.Interfaces;
using Zaka.Core.API.Application.Services;
using Zaka.Core.API.Integration.Twilio;
using Zaka.Core.API.Persistence;
using Zaka.Core.API.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(CreateAccountDto));
// Services.AddMediatR(typeof(CaseCreated));
//var connectionString = builder.Configuration.GetConnectionString("ZakaCoreDbConnection");
//builder.Services.AddDbContext<ZakaCoreContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<ITwilioService, TwilioService>();
builder.Services.AddTransient<IAccountService, AccountService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsPolicy = "_zaka_cors_policy";
builder.Services.AddCors(p =>
                        p.AddPolicy(corsPolicy, builder =>
                        {
                            builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        }));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();

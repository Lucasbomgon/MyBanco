using Microsoft.EntityFrameworkCore;
using MyBanco.Infra;
using MyBanco.Infra.Repository.Carteiras;
using MyBanco.Infra.Repository.Transferencias;
using MyBanco.Models;
using MyBanco.Services.Autorizador;
using MyBanco.Services.Carteiras;
using MyBanco.Services.Notificacao;
using MyBanco.Services.Transferencias;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var serverVersion = new MySqlServerVersion(new Version(8, 0, 46));
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));

builder.Services.AddScoped<ICarteiraRepository, CarteiraRepository>();
builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();
builder.Services.AddScoped<ICarteiraService, CarteiraService>();

builder.Services.AddHttpClient<IAutorizadorService, AutorizadorService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();

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
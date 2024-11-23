using FlappyBird.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WebAPI.Data;
using WebAPI.Service;

var builder = WebApplication.CreateBuilder(args);



//// Configuration de la base de donn�es
builder.Services.AddDbContext<WebAPIContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAPIContext") ?? throw new InvalidOperationException("Connection string 'WebAPIContext' not found."));
    options.UseLazyLoadingProxies();

});

// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<WebAPIContext>();
builder.Services.AddScoped<ScoreService>();


// Configuration de l'authentification avec JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Développement
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "http://localhost:4200",
        ValidIssuer = "https://localhost:7075",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Le secret de la vie éternelle est l'amour"))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });

});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", policy =>
    {

        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();

    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Allow all");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
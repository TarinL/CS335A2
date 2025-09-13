using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using A2Template.Data;
using A2Template.Handler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SupportNonNullableReferenceTypes();
        });
        builder.Services
            .AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, A2AuthHandler>("Authentication", null);
            
        builder.Services.AddDbContext<A2DbContext>(
            options => options.UseSqlite(builder.Configuration["P1DBConnection"]));
        builder.Services.AddScoped<IA2Repo, A2Repo>();
        builder.Services.AddAuthorization(Options =>
        {
            Options.AddPolicy("StaffOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Staff"));
            Options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
        });
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
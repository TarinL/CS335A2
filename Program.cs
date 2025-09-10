using Microsoft.EntityFrameworkCore;
using A2Template.Data;

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

        builder.Services.AddDbContext<A2DbContext>(options => options.UseSqlite(builder.Configuration["P1DBConnection"]));
        builder.Services.AddScoped<IA2Repo, A2Repo>();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
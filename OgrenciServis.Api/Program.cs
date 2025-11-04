
using Microsoft.EntityFrameworkCore;
using OgrenciServis.DataAccess;
using OgrenciServis.Logic.Interface;
using OgrenciServis.Logic.Services;

namespace OgrenciServis.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OkulContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            //1. Çözüm
            builder.Services.AddScoped<IOgrenci,  OgrenciServis.Logic.Services.OgrenciServisImpl>();
            builder.Services.AddScoped<IOgretmen, OgretmenServis>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

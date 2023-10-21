#region Using
using Microsoft.EntityFrameworkCore;
using NomenklatureApi.Data;
#endregion

namespace NomenklatureApi
{
    #region Public Class Program

    /// <summary>
    /// Основной класс api
    /// </summary>
    public class Program
    {
        #region Public Static Methods
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<NomenklatureContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

            builder.Services.AddTransient<INomenklatureRepository, NomenklatureRepository>();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<NomenklatureContext>();

                db.Database.Migrate();
            }

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        #endregion
    }
    #endregion
}
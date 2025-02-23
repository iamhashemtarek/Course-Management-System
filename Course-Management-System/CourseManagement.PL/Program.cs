using CourseManagement.PL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseManagement.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            Console.WriteLine("?? Application Starting...");


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                Console.WriteLine("? Attempting to Seed Database...");

                try
                {
                    DbSeeder.SeedDatabase(serviceProvider);
                    Console.WriteLine("? Database Seeding Completed!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"?? Seeding Failed: {ex.Message}");
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

using CourseManagement.BLL.Interfaces;
using CourseManagement.BLL.Repositories;
using CourseManagement.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.PL
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CourseManagementDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;
            var dbContext = services.GetRequiredService<CourseManagementDbContext>();
            dbContext.Database.Migrate();   
            CourseManagementDbContextSeed.Seed(dbContext);

            Console.WriteLine("?? Application Starting...");


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }           
            //app.UseStaticFiles();

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

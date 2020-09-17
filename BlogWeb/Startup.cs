using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnionDemo.Application;
using OnionDemo.Database;
using OnionDemo.Query;

namespace BlogWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add-Migration Initial -context OnionDemo.Database.BlogContext -Project OnionDemo.Database.Migrations
            // Update-Database Initial -context OnionDemo.Database.BlogContext -Project OnionDemo.Database.Migrations
            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("OnionDemo.Database.Migrations")));

            // Dapper Sql connection
            services.AddTransient<IDatabaseConnectionFactory>(e =>
            {
                return new DatabaseConnectionFactory(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Command and Query
            services.AddScoped<IBlogApplicationService, BlogApplicationService>();
            services.AddScoped<IBlogQueryService, BlogQueryService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
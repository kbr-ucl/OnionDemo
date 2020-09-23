using Blog.Application;
using Blog.Application.Infrastructor;
using Blog.Application.Queries;
using Blog.Database;
using Blog.Database.Query;
using Blog.Database.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.Web
{
    public class StartupSqLite
    {
        public StartupSqLite(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add-Migration Initial -context Blog.Database.BlogContext -Project Blog.Database.Migrations
            // $env:ASPNETCORE_ENVIRONMENT='SqLite'
            // Update-Database Initial -context Blog.Database.BlogContext -Project Blog.Database.Migrations
            services.AddDbContext<BlogContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Blog.Database.Migrations")));

            // Command and Query
            services.AddScoped<IBlogCommand, BlogCommand>();
            services.AddScoped<IBlogQuery, BlogQuery>();
            services.AddScoped<IPostQuery, PostQuery>();

            // Repository
            services.AddScoped<IBlogRepository, BlogRepository>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Blog}/{action=Index}/{id?}");
            });
        }
    }
}
# **Architecture**
## **Onion architecture**
![Onion architecture](Img/Onion-Architecture.png)

## **CQRS architecture**
![CQRS](Img/CQRS-01.png)
## **Blog architecture**

![Blog architecture](Img/Componentdiagram1.png)



# **Visual Studio project structure
![Visual Studio project structure](Img/Visual-Studio-Project-Structure.png)



# **First iteration
## The model


```c#
public class Blog
{
    private readonly List<Post> _posts;

    /// <summary>
    ///     Used only by Entity framework
    /// </summary>
    protected Blog()
    {
    }

    public Blog(Guid id)
    {
        Id = id;
        _posts = new List<Post>();
    }

    public Guid Id { get; }
    public IEnumerable<Post> Posts => _posts;

    public void AddPost(Post post)
    {
        _posts.Add(post);
        Validate();
    }

    private void Validate()
    {
        // Max 10 posts
        if (_posts.Count > 10) throw new MaxPostLimitExceeded($"Maximum postings limit exceeded. You already has {_posts.Count} postings");
    }
}
```


​    
```c#
public class Post
{
    /// <summary>
    ///     Used only by Entity framework
    /// </summary>
    public Post()
    {
    }

    public Post(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
```


## Entity framework

```c#
public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Domain.Model.Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //this will apply configs from separate classes which implemented IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```



```c#
public class BlogConfiguration : IEntityTypeConfiguration<Domain.Model.Blog>
{
    public void Configure(EntityTypeBuilder<Domain.Model.Blog> builder)
    {
        builder.HasKey(a => a.Id);
    }
}
```


```c#
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(a => a.Id);
    }
}
```



From Startup.cs

```c#
    public void ConfigureServices(IServiceCollection services)
    {
        // Add-Migration Initial -context Blog.Database.BlogContext -Project Blog.Database.Migrations
        // Update-Database Initial -context Blog.Database.BlogContext -Project Blog.Database.Migrations
        services.AddDbContext<BlogContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Blog.Database.Migrations")));
```


From appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BlogDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```



## Initial migration
Add-Migration:

![Add-Migration](Img/Add-Migration.png)

Migration project:

![Blog.Database.Migration](Img/Blog-Database-Migration.png)

Update-Database:

![Update-Database](Img/Update-Database.png)

SQL database:

![SQL-Database](Img/SQL-Database.png)

Migrations history

![EF-Migrations-History](Img/EFMigrationsHistory.png)

# **Second iteration**
## The model

```c#
public class Post
{
    /// <summary>
    ///     Used only by Entity framework
    /// </summary>
    public Post()
    {
    }

    public Post(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public string Body { get; private set; }

    public string Title { get; private set; }
}
```
## Second iteration - Migration
Add-Migration:

![Add-Migration](Img/Add-Migration-Second_Iteration.png)

Migration project:

![Blog.Database.Migration](Img/Blog-Database-Migration-Second-Migration.png)

Update-Database:

![Update-Database](Img/Update-Database-Second-Iteration.png)

SQL database:

![SQL-Database](Img/SQL-Database-Second-Iteration.png)

Migrations history

![EF-Migrations-History](Img/EFMigrationsHistory-Second-Iteration.png)

# **Sql server and SqLite**

launchSettings:

![launchSettings.json](Img/launchSettings.png)

launchSettings:

![launchSettings.json](Img/launchSettings.png)


Program.cs
```c#
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup(assemblyName); });
        }
    }
```

Select start enviroment:

![Select start enviroment](Img/select-start-enviroment.png)

## Sql server

appsettings.json

Startup.cs

## SqLite
SqLite start enviroment:

![Select start enviroment](Img/multiple-env.png)

appsettings.SqLite.json:

```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "DataSource = blog.db;"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*"
    }
```
StartupSqLite.cs:

```c#
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
```

### SqLite database migration

:

SqLite start enviroment:

![sqlite database update](Img/sqlite-database-update.png)


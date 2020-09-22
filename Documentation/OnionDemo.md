# **Onion architecture**

![Onion architecture](Img/Onion-Architecture.png)

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
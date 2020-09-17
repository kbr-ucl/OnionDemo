using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnionDemo.Application.Infrastructor;
using OnionDemo.Domain.Model;

namespace OnionDemo.Database
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _db;

        public BlogRepository(BlogContext db)
        {
            _db = db;
        }

        async Task IBlogRepository.Delete(Blog blog)
        {
            var found = await _db.Blogs.FirstOrDefaultAsync(a => a.Id == blog.Id);
            if (found == null) throw new Exception($"Blog not found (id: {blog.Id}");

            _db.Blogs.Remove(found);

            await _db.SaveChangesAsync();
        }

        async Task<Blog> IBlogRepository.Load(Guid id)
        {
            var found = await _db.Blogs.FirstOrDefaultAsync(a => a.Id == id);
            if (found == null) throw new Exception($"Blog not found (id: {id}");

            return found;
        }

        async Task IBlogRepository.Save(Blog blog)
        {
            if(!_db.Blogs.Any(a => a.Id == blog.Id)) _db.Blogs.Add(blog);
            
            await _db.SaveChangesAsync();
        }
    }
}
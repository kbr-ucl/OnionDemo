using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Queries;
using Blog.Application.Queries.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database.Query
{
    public class BlogQuery : IBlogQuery
    {
        private readonly BlogContext _db;

        public BlogQuery(BlogContext db)
        {
            _db = db;
        }

        async Task<BlogDto> IBlogQuery.Get(Guid id)
        {
            return await _db.Blogs.AsNoTracking().Select(a => new BlogDto {Id = a.Id})
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        async Task<IEnumerable<BlogDto>> IBlogQuery.GetAll()
        {
            return await _db.Blogs.AsNoTracking().Select(a => new BlogDto {Id = a.Id}).ToListAsync();
        }
    }
}
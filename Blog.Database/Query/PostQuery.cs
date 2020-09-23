using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application.Queries;
using Blog.Application.Queries.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database.Query
{
    public class PostQuery : IPostQuery
    {
        private readonly BlogContext _db;

        public PostQuery(BlogContext db)
        {
            _db = db;
        }

        async Task<PostDto> IPostQuery.Get(Guid id)
        {
            return await _db.Posts.AsNoTracking()
                .Select(a => new PostDto {Id = a.Id, Title = a.Title, BlogId = a.BlogId, Body = a.Body})
                .FirstOrDefaultAsync(a => a.Id == id);
        }


        async Task<IEnumerable<PostDto>> IPostQuery.GetAllByBlog(Guid blogId)
        {
            return await _db.Posts.AsNoTracking().Where(a => a.BlogId == blogId)
                .Select(a => new PostDto {Id = a.Id, Title = a.Title, BlogId = a.BlogId, Body = a.Body}).ToListAsync();
        }
    }
}
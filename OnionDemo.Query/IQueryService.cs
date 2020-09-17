using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using OnionDemo.Query.Model;

namespace OnionDemo.Query
{
    public interface IBlogQueryService
    {
        Task<IEnumerable<BlogDto>> GetAll();
        Task<BlogDto> Get(Guid id);
    }

    public interface IPostQueryService
    {
        Task<IEnumerable<PostDto>> GetAll();
        Task<IEnumerable<PostDto>> GetAllByBlog(Guid blogId);
        Task<PostDto> Get(Guid id);
    }

    public class BlogQueryService : IBlogQueryService
    {
        private readonly IDatabaseConnectionFactory _db;

        public BlogQueryService(IDatabaseConnectionFactory db)
        {
            _db = db;
        }

        async Task<BlogDto> IBlogQueryService.Get(Guid id)
        {
            var lookup = new Dictionary<Guid, BlogDto>();

            var sql =
                @"select blog.*, post.* from Blogs blog
                    left join Posts post on post.BlogId = blog.Id
                    where blog.Id == @Id";

            var parameters = new {Id = id};

            var connection = await _db.CreateConnectionAsync().ConfigureAwait(false);
            await connection.QueryAsync<BlogDto, PostDto, BlogDto>(sql, (b, p) =>
            {
                BlogDto blog;
                if (!lookup.TryGetValue(b.Id, out blog))
                    lookup.Add(b.Id, blog = b);
                if (b.Posts == null)
                    b.Posts = new List<PostDto>();
                b.Posts.Add(p); /* Add posts to blog */
                return blog;
            }, parameters);

            return lookup.Values.FirstOrDefault();
        }

        async Task<IEnumerable<BlogDto>> IBlogQueryService.GetAll()
        {
            var lookup = new Dictionary<Guid, BlogDto>();

            var sql =
                @"select blog.*, post.* from Blogs blog
                    left join Posts post on post.BlogId = blog.Id";

            var connection = await _db.CreateConnectionAsync().ConfigureAwait(false);
            await connection.QueryAsync<BlogDto, PostDto, BlogDto>(sql, (b, p) =>
            {
                BlogDto blog;
                if (!lookup.TryGetValue(b.Id, out blog))
                    lookup.Add(b.Id, blog = b);
                if (b.Posts == null)
                    b.Posts = new List<PostDto>();
                b.Posts.Add(p); /* Add posts to blog */
                return blog;
            });

            return lookup.Values;
        }
    }
}
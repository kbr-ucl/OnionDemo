using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Blog.Query.Model;

namespace Blog.Query
{
    public class BlogQuery : IBlogQuery
    {
        private readonly IDatabaseConnectionFactory _db;

        public BlogQuery(IDatabaseConnectionFactory db)
        {
            _db = db;
        }

        async Task<BlogDto> IBlogQuery.Get(Guid id)
        {
            var lookup = new Dictionary<Guid, BlogDto>();
            
            var parameters = new DynamicParameters();

            parameters.Add("@Id", id);//, DbType.String, ParameterDirection.Input, customerId.Length);

            var sql =
                @"select blog.*, post.* from Blogs blog
                    left join Posts post on post.BlogId = blog.Id
                    where blog.Id = @Id";
                       

            var connection = await _db.CreateConnectionAsync().ConfigureAwait(false);
            await connection.QueryAsync<BlogDto, PostDto, BlogDto>(sql, (b, p) =>
            {
                BlogDto blog;
                if (!lookup.TryGetValue(b.Id, out blog))
                    lookup.Add(b.Id, blog = b);
                if (b.Posts == null)
                    b.Posts = new List<PostDto>();
                if(p != null)
                    b.Posts.Add(p); /* Add posts to blog */
                return blog;
            }, parameters);

            return lookup.Values.FirstOrDefault();
        }

        async Task<IEnumerable<BlogDto>> IBlogQuery.GetAll()
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
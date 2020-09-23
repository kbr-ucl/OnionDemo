using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Application.Queries.Model;

namespace Blog.Application.Queries
{
    public interface IPostQuery
    {
        Task<IEnumerable<PostDto>> GetAllByBlog(Guid blogId);
        Task<PostDto> Get(Guid id);
    }
}
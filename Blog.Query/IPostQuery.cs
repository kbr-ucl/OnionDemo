using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Query.Model;

namespace Blog.Query
{
    public interface IPostQuery
    {
        Task<IEnumerable<PostDto>> GetAll();
        Task<IEnumerable<PostDto>> GetAllByBlog(Guid blogId);
        Task<PostDto> Get(Guid id);
    }
}
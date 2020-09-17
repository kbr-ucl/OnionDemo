using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnionDemo.Query.Model;

namespace OnionDemo.Query
{
    public interface IPostQuery
    {
        Task<IEnumerable<PostDto>> GetAll();
        Task<IEnumerable<PostDto>> GetAllByBlog(Guid blogId);
        Task<PostDto> Get(Guid id);
    }
}
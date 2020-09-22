using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Query.Model;

namespace Blog.Query
{
    public interface IBlogQuery
    {
        Task<IEnumerable<BlogDto>> GetAll();
        Task<BlogDto> Get(Guid id);
    }
}
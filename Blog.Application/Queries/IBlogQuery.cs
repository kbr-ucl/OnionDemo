using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Application.Queries.Model;

namespace Blog.Application.Queries
{
    public interface IBlogQuery
    {
        Task<IEnumerable<BlogDto>> GetAll();
        Task<BlogDto> Get(Guid id);
    }
}
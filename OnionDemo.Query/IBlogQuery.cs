using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnionDemo.Query.Model;

namespace OnionDemo.Query
{
    public interface IBlogQuery
    {
        Task<IEnumerable<BlogDto>> GetAll();
        Task<BlogDto> Get(Guid id);
    }
}
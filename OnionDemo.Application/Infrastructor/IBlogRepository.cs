using System;
using System.Threading.Tasks;
using OnionDemo.Domain.Model;

namespace OnionDemo.Application.Infrastructor
{
    public interface IBlogRepository
    {
        Task<Blog> Load(Guid id);
        Task Save(Blog blog);
        Task Delete(Blog blog);
    }
}
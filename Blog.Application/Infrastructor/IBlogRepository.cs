using System;
using System.Threading.Tasks;

namespace Blog.Application.Infrastructor
{
    public interface IBlogRepository
    {
        Task<Domain.Model.Blog> Load(Guid id);
        Task Save(Domain.Model.Blog blog);
        Task Delete(Domain.Model.Blog blog);
    }
}
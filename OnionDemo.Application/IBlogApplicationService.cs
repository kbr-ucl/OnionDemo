using System;
using System.Threading.Tasks;
using OnionDemo.Application.Commands;

namespace OnionDemo.Application
{
    public interface IBlogApplicationService
    {
        Task Execute(Command.AddPostToBlog command);
        Task Execute(Command.UpdatePost command);
        Task Execute(Command.CreateBlog command);
        Task Execute(Command.UpdateBlog command);
        Task Execute(Command.DeleteBlog command);
    }

    public class BlogApplicationService : IBlogApplicationService
    {
        async Task IBlogApplicationService.Execute(Command.AddPostToBlog command)
        {
            throw new NotImplementedException();
        }

        async Task IBlogApplicationService.Execute(Command.UpdatePost command)
        {
            throw new NotImplementedException();
        }

        async Task IBlogApplicationService.Execute(Command.CreateBlog command)
        {
            throw new NotImplementedException();
        }

        async Task IBlogApplicationService.Execute(Command.UpdateBlog command)
        {
            throw new NotImplementedException();
        }

        async Task IBlogApplicationService.Execute(Command.DeleteBlog command)
        {
            throw new NotImplementedException();
        }
    }
}
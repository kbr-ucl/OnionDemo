using System.Threading.Tasks;
using OnionDemo.Application.Commands;

namespace OnionDemo.Application
{
    public interface IBlogCommand
    {
        Task Execute(Command.AddPostToBlog command);
        Task Execute(Command.UpdatePost command);
        Task Execute(Command.CreateBlog command);
        Task Execute(Command.UpdateBlog command);
        Task Execute(Command.DeleteBlog command);
    }
}
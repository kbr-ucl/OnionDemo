using System.Threading.Tasks;
using OnionDemo.Application.Commands;

namespace OnionDemo.Application
{
    public interface IBlogApplicationService
    {
        Task Execute(Command.AddPostToBlog command);
        Task Execute(Command.UpdatePost command);
        void Execute(Command.CreateBlog command);
        void Execute(Command.UpdateBlog command);
        void Execute(Command.DeleteBlog command);
    }
}
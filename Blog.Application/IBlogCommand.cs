using System.Threading.Tasks;
using Blog.Application.Commands;

namespace Blog.Application
{
    public interface IBlogCommand
    {
        Task Execute(Command.CreatePost command);
        Task Execute(Command.CreateBlog command);

        // iteration 3
        Task Execute(Command.UpdatePost command);

        // iteration 3
        Task Execute(Command.DeletePost deletePost);

        //TODO: Later
        //Task Execute(Command.UpdateBlog command);

        // iteration 4
        Task Execute(Command.DeleteBlog command);
    }
}
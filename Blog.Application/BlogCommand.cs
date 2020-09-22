using System;
using System.Threading.Tasks;
using Blog.Application.Commands;
using Blog.Application.Infrastructor;
using Blog.Domain.Model;

namespace Blog.Application
{
    public class BlogCommand : IBlogCommand
    {
        private readonly IBlogRepository _repository;

        public BlogCommand(IBlogRepository repository)
        {
            _repository = repository;
        }

        async Task IBlogCommand.Execute(Command.AddPostToBlog command)
        {
            var blog = await _repository.Load(command.BlogId);
            blog.AddPost(new Post(command.Post.Id, command.BlogId, command.Post.Title, command.Post.Body));
            await _repository.Save(blog);
        }

        async Task IBlogCommand.Execute(Command.UpdatePost command)
        {
            var blog = await _repository.Load(command.BlogId);

            await _repository.Save(blog);
        }

        async Task IBlogCommand.Execute(Command.CreateBlog command)
        {
            var blog = new Domain.Model.Blog(Guid.NewGuid());
            await _repository.Save(blog);
        }

        async Task IBlogCommand.Execute(Command.UpdateBlog command)
        {
            var blog = await _repository.Load(command.BlogId);

            await _repository.Save(blog);
        }

        async Task IBlogCommand.Execute(Command.DeleteBlog command)
        {
            var blog = await _repository.Load(command.BlogId);

            await _repository.Delete(blog);
        }
    }
}
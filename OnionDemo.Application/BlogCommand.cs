using System;
using System.Threading.Tasks;
using OnionDemo.Application.Commands;
using OnionDemo.Application.Infrastructor;
using OnionDemo.Domain.Model;

namespace OnionDemo.Application
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
            blog.AddPost(new Post(command.Post.Id));
            await _repository.Save(blog);
        }

        async Task IBlogCommand.Execute(Command.UpdatePost command)
        {
            var blog = await _repository.Load(command.BlogId);

            await _repository.Save(blog);
        }

        async Task IBlogCommand.Execute(Command.CreateBlog command)
        {
            var blog = new Blog(Guid.NewGuid());
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
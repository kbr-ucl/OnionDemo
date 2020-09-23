using System;

namespace Blog.Domain.Model
{
    public class Post
    {
        /// <summary>
        ///     Used only by Entity framework
        /// </summary>
        public Post()
        {
        }

        public Post(Guid id, Guid blogId, string title, string body)
        {
            Id = id;
            BlogId = blogId;
            Title = title;
            Body = body;
        }

        public Guid Id { get; }

        public Guid BlogId { get; private set; }

        public string Body { get; private set; }

        public string Title { get; private set; }
    }
}
using System;

namespace Blog.Domain.Model
{
    public class Post
    {
        /// <summary>
        ///     Anvendes af Entity framework
        /// </summary>
        public Post()
        {
        }

        public Post(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
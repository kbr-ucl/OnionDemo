using System;
using System.Collections.Generic;
using OnionDemo.Domain.OnionDemoExceptions;

namespace OnionDemo.Domain.Model
{
    public class Blog
    {
        private readonly List<Post> _posts;

        /// <summary>
        ///     Anvendes af Entity framework
        /// </summary>
        protected Blog()
        {
        }

        public Blog(Guid id)
        {
            Id = id;
            _posts = new List<Post>();
        }

        public Guid Id { get; }
        public IEnumerable<Post> Posts => _posts;

        public void AddPost(Post post)
        {
            _posts.Add(post);
            Validate();
        }

        private void Validate()
        {
            // Max 10 posts
            if (_posts.Count > 10) throw new MaxPostLimitExceeded($"Der er allerede {_posts.Count}");
        }
    }
}
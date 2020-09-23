using System;
using System.Collections.Generic;
using Blog.Domain.BlogExceptions;

namespace Blog.Domain.Model
{
    public class Blog
    {
        private readonly List<Post> _posts = new List<Post>();

        public Blog(Guid id)
        {
            Id = id;
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
            if (_posts.Count > 10)
                throw new MaxPostLimitExceeded(
                    $"Maximum postings limit exceeded. You already has {_posts.Count} postings");
        }
    }
}
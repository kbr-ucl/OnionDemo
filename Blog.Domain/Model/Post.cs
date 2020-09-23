using System;
using System.ComponentModel.DataAnnotations;

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

            Validate();
        }

        private void Validate()
        {

                if (Title.Length > 50) throw new TitleMaxLengthExceeded($"Maximum postings limit exceeded. You already has {_posts.Count} postings");
            
        }

        public Guid Id { get; }

        public Guid BlogId { get; private set; }

        public string Body { get; private set; }

        public string Title { get; private set; }
    }

    internal class TitleMaxLengthExceeded : Exception
    {
        public TitleMaxLengthExceeded(string s) : base(s)
        {
            
        }
    }
}
using System;

namespace Blog.Domain.Model
{
    public class Post
    {
        public Post(Guid id, Guid blogId, string title, string body)
        {
            Id = id;
            BlogId = blogId;
            Title = title;
            Body = body;
            Validate();
        }

        public Guid Id { get; }

        public Guid BlogId { get; }

        public string Body { get; private set; }

        public string Title { get; private set; }


        public void UpdateTitle(string title)
        {
            Title = title;
            Validate();
        }

        public void UpdateBody(string body)
        {
            Body = body;
            Validate();
        }


        private void Validate()
        {
            if (Title.Length > 50)
                throw new TitleMaxLengthExceeded(
                    $"Maximum Title length exceeded. Your title is {Title.Length} characters. Max is 50");
        }
    }

    internal class TitleMaxLengthExceeded : Exception
    {
        public TitleMaxLengthExceeded(string s) : base(s)
        {
        }
    }
}
using System;

namespace Blog.Application.Dto
{
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public string Title { get; set; }
    }
}
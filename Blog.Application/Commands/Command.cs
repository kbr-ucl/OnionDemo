using System;
using Blog.Application.Dto;

namespace Blog.Application.Commands
{
    public class Command
    {
        public class CreatePost
        {
            public Guid BlogId { get; set; }
            public PostDto Post { get; set; }
        }

        // iteration 3
        public class UpdatePost
        {
            public Guid BlogId { get; set; }
            public PostDto Post { get; set; }
        }

        // iteration 3
        public class DeletePost
        {
            public Guid BlogId { get; set; }
            public PostDto Post { get; set; }
        }

        public class CreateBlog
        {
        }

        //TODO: Later
        //public class UpdateBlog
        //{
        //    public Guid BlogId { get; set; }
        //}

        // iteration 4
        public class DeleteBlog
        {
            public Guid BlogId { get; set; }
        }
    }
}
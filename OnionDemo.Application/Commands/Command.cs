﻿using System;
using OnionDemo.Application.Dto;

namespace OnionDemo.Application.Commands
{
    public class Command
    {
        public class AddPostToBlog
        {
            public Guid BlogId { get; set; }
            public PostDto Post { get; set; }
        }

        public class UpdatePost
        {
            public Guid BlogId { get; set; }
            public PostDto Post { get; set; }
        }

        public class CreateBlog
        {
            public Guid BlogId { get; set; }
        }

        public class UpdateBlog 
        {
            public Guid BlogId { get; set; }
        }

        public class DeleteBlog 
        {
            public Guid BlogId { get; set; }
        }
    }
}
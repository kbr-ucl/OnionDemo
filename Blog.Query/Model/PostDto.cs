﻿using System;

namespace Blog.Query.Model
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }

        public string Body { get;  set; }

        public string Title { get;  set; }
    }
}
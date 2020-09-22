using System;
using System.Collections.Generic;

namespace Blog.Query.Model
{
    public class BlogDto
    {


        public Guid Id { get; set; }
        public List<PostDto> Posts { get; set; }

    }
}
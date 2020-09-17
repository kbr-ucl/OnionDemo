using System;
using System.Collections.Generic;

namespace OnionDemo.Query.Model
{
    public class BlogDto
    {


        public Guid Id { get; set; }
        public List<PostDto> Posts { get; set; }

    }
}
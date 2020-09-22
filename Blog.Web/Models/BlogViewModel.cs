using System;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
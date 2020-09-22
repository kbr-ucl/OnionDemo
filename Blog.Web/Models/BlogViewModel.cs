using System;
using System.Collections.Generic;

namespace BlogWeb.Models
{
    public class BlogViewModel
    {
        public Guid Id { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
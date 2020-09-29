using System;

namespace Blog.Web.Models
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
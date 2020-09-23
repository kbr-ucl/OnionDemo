using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Query.Model;
using Blog.Web.Models;

namespace Blog.Web.Mappers
{
    public class Mapper
    {
        public static IEnumerable<BlogViewModel> Map(IEnumerable<BlogDto> data)
        {
            if (data == null) return null;

            var res = new List<BlogViewModel>();
            data.ToList().ForEach(a => res.Add(Map(a)));
            return res;
        }

        public static BlogViewModel Map(BlogDto data)
        {
            if (data == null) return null;

            return new BlogViewModel {Id = data.Id, Posts = new List<PostViewModel>(Map(data.Posts))};
        }

        public static IEnumerable<PostViewModel> Map(List<PostDto> data)
        {
            if (data == null) return null;

            var res = new List<PostViewModel>();
            data.ToList().ForEach(a => res.Add(Map(a)));
            return res;
        }

        public static PostViewModel Map(PostDto data)
        {
            if (data == null) return null;

            return new PostViewModel
            {
                BlogId = data.BlogId,
                Id = data.Id,
                Body = data.Body,
                Title = data.Title
            };
        }

        public static Application.Dto.PostDto Map(PostViewModel post)
        {
            return new Application.Dto.PostDto {Id = Guid.NewGuid(), Title = post.Title, Body = post.Body};
        }
    }
}
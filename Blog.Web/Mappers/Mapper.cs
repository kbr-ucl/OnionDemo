using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Application.Queries.Model;
using Blog.Web.Models;

namespace Blog.Web.Mappers
{
    public class Mapper
    {
        public static IEnumerable<BlogViewModel> Map(IEnumerable<BlogDto> data)
        {
            var res = new List<BlogViewModel>();
            if (data == null) return res;

            data.ToList().ForEach(a => res.Add(Map(a)));
            return res;
        }

        public static BlogViewModel Map(BlogDto data)
        {
            if (data == null) return null;

            return new BlogViewModel {Id = data.Id, Posts = new List<PostViewModel>(Map(data.Posts))};
        }

        public static IEnumerable<PostViewModel> Map(IEnumerable<PostDto> data)
        {
            var res = new List<PostViewModel>();
            if (data == null) return res;

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
                Title = data.Title,
                RowVersion = data.RowVersion
            };
        }

        public static Application.Dto.PostDto Map(PostViewModel post)
        {
            return new Application.Dto.PostDto {Id = Guid.NewGuid(), Title = post.Title, Body = post.Body};
        }
    }
}
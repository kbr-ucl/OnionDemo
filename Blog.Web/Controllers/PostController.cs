using System;
using System.Threading.Tasks;
using Blog.Application;
using Blog.Application.Commands;
using Blog.Application.Queries;
using Blog.Web.Mappers;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogCommand _command;
        private readonly IPostQuery _query;


        public PostController(IPostQuery query, IBlogCommand command)
        {
            _query = query;
            _command = command;
        }

        // GET
        public async Task<IActionResult> Index(Guid blogId)
        {
            var data = await _query.GetAllByBlog(blogId);
            var viewModel = Mapper.Map(data);
            ViewData["BlogId"] = blogId;
            return View(viewModel);
        }

        public IActionResult Create(Guid blogId)
        {
            ViewData["BlogId"] = blogId;
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                await _command.Execute(new Command.AddPostToBlog {BlogId = post.BlogId, Post = Mapper.Map(post)});
                return RedirectToAction(nameof(Index), new {blogId = post.BlogId});
            }

            return View(post);
        }
    }
}
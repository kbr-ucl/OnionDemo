using System;
using System.Threading.Tasks;
using Blog.Application;
using Blog.Application.Commands;
using Blog.Application.Dto;
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

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                await _command.Execute(new Command.CreatePost {BlogId = post.BlogId, Post = Mapper.Map(post)});
                return RedirectToAction(nameof(Index), new {blogId = post.BlogId});
            }

            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var viewModel = Mapper.Map(await _query.Get(id.Value));
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PostViewModel post)
        {
            if (id != post.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _command.Execute(new Command.UpdatePost
                    {BlogId = post.BlogId, Post = new PostDto {Id = post.Id, Title = post.Title, Body = post.Body}});
                return RedirectToAction(nameof(Index), new {blogId = post.BlogId});
            }

            return View(post);
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var viewModel = Mapper.Map(await _query.Get(id.Value));
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        // POST: Blog/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(PostViewModel post)
        {
            //TODO - iteration 4
            await _command.Execute(new Command.DeletePost
                {BlogId = post.BlogId, Post = new PostDto {Id = post.Id, Title = post.Title, Body = post.Body}});
            return RedirectToAction(nameof(Index), new {blogId = post.BlogId});
        }
    }
}
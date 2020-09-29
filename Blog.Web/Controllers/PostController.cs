using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Application;
using Blog.Application.Commands;
using Blog.Application.Dto;
using Blog.Application.Queries;
using Blog.Domain.Model;
using Blog.Web.Mappers;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                try
                {
                    await _command.Execute(new Command.UpdatePost
                    {
                        BlogId = post.BlogId, Post = new PostDto {Id = post.Id, Title = post.Title, Body = post.Body, RowVersion = post.RowVersion}
                    });
                    return RedirectToAction(nameof(Index), new {blogId = post.BlogId});
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Post) entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Post) databaseEntry.ToObject();

                        if (databaseValues.Title != clientValues.Title)
                            ModelState.AddModelError("Title", $"Current value: {databaseValues.Title}");
                        if (databaseValues.Body != clientValues.Body)
                            ModelState.AddModelError("Body", $"Current value: {databaseValues.Body}");

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                                               + "was modified by another user after you got the original value. The "
                                                               + "edit operation was canceled and the current values in the database "
                                                               + "have been displayed. If you still want to edit this record, click "
                                                               + "the Save button again. Otherwise click the Back to List hyperlink.");
                        post.RowVersion = databaseValues.RowVersion;
                    }
                }
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
            // iteration 4
            await _command.Execute(new Command.DeletePost
                {BlogId = post.BlogId, Post = new PostDto {Id = post.Id, Title = post.Title, Body = post.Body}});
            return RedirectToAction(nameof(Index), new {blogId = post.BlogId});
        }
    }
}
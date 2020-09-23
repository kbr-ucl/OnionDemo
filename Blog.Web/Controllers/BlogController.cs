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
    public class BlogController : Controller
    {
        private readonly IBlogCommand _command;
        private readonly IBlogQuery _query;

        public BlogController(IBlogQuery query, IBlogCommand command)
        {
            _query = query;
            _command = command;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var viewModel = Mapper.Map(await _query.GetAll());
            return View(viewModel);
        }


        // GET: Blog/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var viewModel = Mapper.Map(await _query.Get(id.Value));
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] BlogViewModel blog)
        {
            if (ModelState.IsValid)
            {
                await _command.Execute(new Command.CreateBlog());
                return RedirectToAction(nameof(Index));
            }

            return View(blog);
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var viewModel = Mapper.Map(await _query.Get(id.Value));
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id")] BlogViewModel blog)
        {
            if (id != blog.Id) return NotFound();

            if (ModelState.IsValid)
                //TODO - iteration 4
                //await _command.Execute(new Command.UpdateBlog {BlogId = id});
                return RedirectToAction(nameof(Index));

            return View(blog);
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //TODO - iteration 4
            // await _command.Execute(new Command.DeleteBlog {BlogId = id});
            return RedirectToAction(nameof(Index));
        }
    }
}
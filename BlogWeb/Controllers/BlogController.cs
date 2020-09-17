using System;
using System.Threading.Tasks;
using BlogWeb.Mappers;
using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using OnionDemo.Application;
using OnionDemo.Application.Commands;
using OnionDemo.Query;

namespace BlogWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogApplicationService _command;
        private readonly IBlogQueryService _query;

        public BlogController(IBlogQueryService query, IBlogApplicationService command)
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
                _command.Execute(new Command.CreateBlog {BlogId = Guid.NewGuid()});
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
            {
                _command.Execute(new Command.UpdateBlog {BlogId = id});
                return RedirectToAction(nameof(Index));
            }

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
            _command.Execute(new Command.DeleteBlog {BlogId = id});
            return RedirectToAction(nameof(Index));
        }
    }
}
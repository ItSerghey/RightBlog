using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RightBlog.Core.Data;
using RightBlog.Core.Models;

namespace RightBlog.Core.Controllers
{
    public class ContentController : Controller
    {
        private readonly ArticleContext _articleContext;

        public ContentController(ArticleContext articleContext)
        {
            _articleContext = articleContext;
        }

        // GET: TestModels
        public async Task<IActionResult> Index()
        {
            var a = _articleContext.Articles;
            return View();
           // return View(await _context.TestModel.ToListAsync());
        }

        public async Task<ActionResult> GetArticles()
        {
            var articles = await _articleContext.GetArticles(null, null);
            //  var model = new ComputerList { Computers = computers, Filter = filter };

            return View("ContentPreview", articles);
        }

        public async Task<ActionResult> GetImage(string id)
        {
            var image = await _articleContext.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }

        // GET: TestModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return View();
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var testModel = await _context.TestModel
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (testModel == null)
            //{
            //    return NotFound();
            //}

            //return View(testModel);
        }

        // GET: TestModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MyProperty,MyProperty2")] TestModel testModel)
        {
            if (ModelState.IsValid)
            {
                testModel.Id = Guid.NewGuid();
               // _context.Add(testModel);
               // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testModel);
        }

        // GET: TestModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var testModel = await _context.TestModel.FindAsync(id);
            //if (testModel == null)
            //{
            //    return NotFound();
            //}
            // return View(testModel);
            return View();
        }

        // POST: TestModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MyProperty,MyProperty2")] TestModel testModel)
        {
            if (id != testModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  //  _context.Update(testModel);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestModelExists(testModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testModel);
        }

        // GET: TestModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var testModel = await _context.TestModel
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (testModel == null)
            //{
            //    return NotFound();
            //}

           // return View(testModel);
            return View();
        }

        // POST: TestModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var testModel = await _context.TestModel.FindAsync(id);
            //_context.TestModel.Remove(testModel);
          //  await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestModelExists(Guid id)
        {
            return true; // _context.TestModel.Any(e => e.Id == id);
        }
    }
}

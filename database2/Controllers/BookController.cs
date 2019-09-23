using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using database2.Data;
using database2.Data.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace database2.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var model = await _context.Books.ToListAsync();
            return View(model);
        }
        [Authorize]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var oldBook = await _context.Books.AnyAsync(b => b.Name == model.Name);
                if (oldBook)
                {
                    return View();
                }
                else
                {
                    _context.Books.Add(Model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> BookEdit(int Id)
        {

            var book = await _context.Books.FirstOrDefaultAsync(anon => anon.Id == Id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var book = await _context.Books.FirstOrDefaultAsync(anon => anon.Id == model.Id);
                if (book != null)
                {
                    book.Name = model.Name;
                    book.Price = model.Price;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> BookDelete(int Id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(anon => anon.Id == Id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Controllers
{
    [Route("/api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MyBookStoreContext _db;

        public BooksController(MyBookStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var books = _db.Books.ToList();
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Books book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Add(book);
            _db.SaveChanges();
            return CreatedAtAction("Get", new { id = book.Id }, book);
        }
    }
}

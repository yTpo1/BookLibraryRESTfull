using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Controllers
{
    //[ApiController]
    [Produces("application/json")]
    [Route("/api/books")]
    public class BooksController : ControllerBase
    {
        // reference to the db context that actions can use
        private readonly MyBookStoreContext _db;

        // Injecting db context through the constructer of the controller
        public BooksController(MyBookStoreContext db)
        {
            // db context
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBook()
        {
            //var books = _db.Books.ToList();
            //return Ok(books);
            return new ObjectResult(_db.Books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            return Ok(book);
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

        //[HttpPost]
        //public IActionResult Post([FromBody]Books book)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _db.Add(book);
        //    _db.SaveChanges();
        //    return CreatedAtAction("Get", new { id = book.Id }, book);
        //}
    }
}

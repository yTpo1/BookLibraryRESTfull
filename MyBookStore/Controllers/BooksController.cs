using Microsoft.AspNetCore.Http;
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

        private bool BookExists(int id)
        {
            return _db.Books.Any(e => e.Id == id);
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
        public async Task<IActionResult> Post([FromBody]Books book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //_db.Add(book);
            //await _db.SaveChangesAsync();
            //return CreatedAtAction("Get", new { id = book.Id }, book);
            _db.Books.Add(book);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = book.Id }, book);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Books book)
        //{
        //    _db.Entry(book).State = EntityState.Modified;
        //    await _db.SaveChangesAsync();

        //    return Ok(book);
        //}

        [HttpPut("{id}")]
        [Produces(typeof(Books))]
        public async Task<IActionResult> PutProduct([FromRoute] string id, [FromBody] Books book)
        {
            int n_id = Int32.Parse(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (n_id != book.Id)
            {
                return BadRequest();
            }

            _db.Entry(book).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                return Ok(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(n_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return Ok(book);
        }
    }
}

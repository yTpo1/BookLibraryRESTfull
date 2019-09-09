using Microsoft.AspNetCore.Mvc;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Controllers
{
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly MyBookStoreContext _db;

        public UserController(MyBookStoreContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _db.User.ToList();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Add(user);
            _db.SaveChanges();
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpGet("readbook/{id}")]
        public IActionResult GetReadBookAndRate(int id)
        {
            var booksRead = _db.BooksRead.FirstOrDefault(x => x.Id == id);
            return Ok(booksRead);
        }

        [HttpPost("readbook")]
        public IActionResult PostReadBookAndRate([FromBody]BooksRead booksRead)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Add(booksRead);
            _db.SaveChanges();
            return CreatedAtAction("Get", new { id = booksRead.Id }, booksRead);
        }
    }
}

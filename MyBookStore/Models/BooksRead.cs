using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyBookStore.Models
{
    public class BooksRead
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public virtual User User { get; set; }
        public virtual Books Books { get; set; }
    }
}

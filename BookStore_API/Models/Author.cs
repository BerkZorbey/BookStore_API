using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore_API.Models
{
    public class Author
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public List<Book> Books { get; set; }
    }
}

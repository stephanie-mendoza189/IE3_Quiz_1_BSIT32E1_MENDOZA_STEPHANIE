using Microsoft.AspNetCore.Mvc;
using LibraryCatalog.Api.Models;

namespace LibraryCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private static readonly List<Book> _books = new()
    {
        new Book { Id = 1, Title = "Clean Architecture", Author = "Robert C. Martin", IsAvailable = true, YearPublished = 2017 },
        new Book { Id = 2, Title = "Design Patterns", Author = "Erich Gamma", IsAvailable = false, YearPublished = 1994 }
    };



    // GET: api/books -> Returns 200 OK
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_books);
    }



    // GET: api/books/{id} -> Returns 200 OK or 404 Not Found
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound(new { message = $"Book with ID {id} was not found." });
        }

        return Ok(book);
    }





    // POST: api/books -> Returns 201 Created or 400 Bad Request
    [HttpPost]
    public IActionResult Create(Book newBook)
    {
        // [ApiController] automatically validates annotations and returns 400 Bad Request if invalid.
        newBook.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
        _books.Add(newBook);

        return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
    }




    // PUT: api/books/{id} -> Returns 204 No Content, 400 Bad Request, or 404 Not Found
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Book updatedBook)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound(new { message = $"Book with ID {id} was not found." });
        }

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.IsAvailable = updatedBook.IsAvailable;
        book.YearPublished = updatedBook.YearPublished;

        return NoContent();
    }



    // DELETE: api/books/{id} -> Returns 204 No Content or 404 Not Found
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound(new { message = $"Book with ID {id} was not found." });
        }

        _books.Remove(book);

        return NoContent();
    }
}
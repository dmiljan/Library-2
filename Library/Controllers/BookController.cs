using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBook _bookService;

        public BookController(IBook bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _bookService.GetBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var book = await _bookService.GetBook(id);
            if(book != null)
            {
                return Ok(book);
            }
            return NotFound($"Book with Id: {id} was not found.");
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var book = await _bookService.GetBookByName(name);
            if(book != null)
            {
                return Ok(book);
            }
            return NotFound($"Book with Name: {name} was not found.");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBook(book);

                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + book.Id, book);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                var existingBook = await _bookService.GetBook(id);

                if (existingBook != null)
                {
                    book.Id = existingBook.Id;
                    await _bookService.EditBook(book);
                }
                return Ok(book);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var book = _applicationDbContext.Books.Find(id);
            var book = await _bookService.GetBook(id);
            if(book != null)
            {
                await _bookService.DeleteBook(book);
                return Ok();
            }
            return NotFound($"Book with Id: {id} was not found");
        }
    }
}
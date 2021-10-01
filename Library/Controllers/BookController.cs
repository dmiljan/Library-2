﻿using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            var book = _bookService.GetBook(id);
            if(book != null)
            {
                return Ok(book);
            }
            return NotFound($"Book with Id: {id} was not found.");
        }

        [HttpGet("search/{name}")]
        public IActionResult Search(string name)
        {
            var book = _bookService.GetBookByName(name);
            if(book != null)
            {
                return Ok(book);
            }
            return NotFound($"Book with Name: {name} was not found.");
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.AddBook(book);

                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + book.Id, book);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                var existingBook = _bookService.GetBook(id);

                if (existingBook != null)
                {
                    book.Id = existingBook.Id;
                    _bookService.EditBook(book);
                }
                return Ok(book);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var book = _applicationDbContext.Books.Find(id);
            var book = _bookService.GetBook(id);
            if(book != null)
            {
                _bookService.DeleteBook(book);
                return Ok();
            }
            return NotFound($"Book with Id: {id} was not found");
        }
    }
}
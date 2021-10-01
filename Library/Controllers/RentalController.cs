using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private IRental _rentalService;

        public RentalController(IRental rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_rentalService.GetRents());
        }

        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            var rent = _rentalService.GetRent(id);
            if(rent != null)
            {
                return Ok(rent);
            }
            return NotFound($"Rental with Id: {id} was not found.");
        }

        [HttpPost]
        public IActionResult Create(Rental rental)//Rent book
        {
            if (ModelState.IsValid)
            {
                _rentalService.RentBook(rental);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + rental.Id, rental);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) //Return book
        {
            var rental = _rentalService.GetRent(id);
            
            if(rental != null)
            {
                _rentalService.ReturnBook(rental);
                return Ok();
            }
            return NotFound($"Rent with Id: {id} was not found");
        }
    }
}
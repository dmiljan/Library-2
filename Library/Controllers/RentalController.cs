using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            return Ok(await _rentalService.GetRents());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> View(int id)
        {
            var rent = await _rentalService.GetRent(id);
            if(rent != null)
            {
                return Ok(rent);
            }
            return NotFound($"Rental with Id: {id} was not found.");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rental rental)//Rent book
        {
            if (ModelState.IsValid)
            {
                await _rentalService.RentBook(rental);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + rental.Id, rental);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) //Return book
        {
            var rental = await _rentalService.GetRent(id);
            
            if(rental != null)
            {
                await _rentalService.ReturnBook(rental);
                return Ok();
            }
            return NotFound($"Rent with Id: {id} was not found");
        }

        [HttpGet("search/{firstName}/{lastName}")]
        public async Task<IActionResult> Search(string firstName, string lastName)
        {
            var rentals = await _rentalService.GetRentsByNameMember(firstName, lastName);

            if(rentals != null)
            {
                return Ok(rentals);
            }
            return NotFound("Member doesn't have any rented books.");
        }
    }
}
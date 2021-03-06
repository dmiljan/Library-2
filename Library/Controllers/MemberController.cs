using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMember _memberService;
        public MemberController(IMember memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _memberService.GetMembers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> View(int id)
        {
            var member = await _memberService.GetMember(id);
            if(member != null)
            {
                return Ok(member);
            }
            return NotFound($"Member with Id: {id} was not found");
        }

        [HttpGet("search/{firstName}/{lastName}")]
        public async Task<IActionResult> Search(string firstName, string lastName)
        {
            var member = await _memberService.GetMemberByName(firstName, lastName);
            if(member != null)
            {
                return Ok(member);
            }
            return NotFound($"Member with Name: {firstName} {lastName} was not found.");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                await _memberService.AddMember(member);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + member.Id, member);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Edit(int id, Member member)
        {
            if (ModelState.IsValid)
            {
                var existingMember = await _memberService.GetMember(id);

                if(existingMember != null)
                {
                    member.Id = existingMember.Id;
                    await _memberService.EditMember(member);

                    return Ok(member);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberService.GetMember(id);

            if(member != null)
            {
                await _memberService.DeleteMember(member);
                return Ok();
            }
            return NotFound($"Book with Id: {id} was not found");
        }

        [HttpGet("rentedBooks/{id}")]
        public async Task<IActionResult> RentedBooks(int id) 
        {
            var rentedBooks = await _memberService.RentedBooks(id);

            if (rentedBooks != null)
            {
                return Ok(rentedBooks);
            }
            return NotFound("Member doesn't have any rented books.");
        }
    }
}
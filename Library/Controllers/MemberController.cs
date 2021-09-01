using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return Ok(_memberService.GetMembers());
        }

        [HttpGet("{id}")]
        public IActionResult View(int id)
        {
            var member = _memberService.GetMember(id);
            if(member != null)
            {
                return Ok(member);
            }
            return NotFound($"Member with Id: {id} was not fount");
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                _memberService.AddMember(member);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + member.Id, member);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, Member member)
        {
            if (ModelState.IsValid)
            {
                var existingMember = _memberService.GetMember(id);

                if(existingMember != null)
                {
                    member.Id = existingMember.Id;
                    _memberService.EditMember(member);

                    return Ok(member);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = _memberService.GetMember(id);

            if(member != null)
            {
                _memberService.DeleteMember(member);
                return Ok();
            }
            return NotFound($"Book with Id: {id} was not found");
        }
    }
}
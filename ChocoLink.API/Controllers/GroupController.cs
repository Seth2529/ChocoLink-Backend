using ChocoLink.API.ViewModels;
using ChocoLink.Data.EntityFramework;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IService;
using Microsoft.AspNetCore.Mvc;

namespace ChocoLink.API.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly Context db = new Context();
        public GroupController(IGroupService userService)
        {
            _groupService = userService;
        }
        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup([FromForm] NewGroupViewModel group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] photoBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await group.Photo.CopyToAsync(memoryStream);
                        photoBytes = memoryStream.ToArray();
                    }

                    Group add = new()
                    {
                        Photo = photoBytes,
                        GroupName = group.GroupName,
                        NumberParticipants = group.NumberParticipants,
                        Value = group.Value,
                        DateDiscover = group.DateDiscover,
                        Description = group.Description,
                    };

                    _groupService.AddGroup(add);
                    return Ok("Adicionado com Sucesso");
                }
                return BadRequest("Dados inválidos.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("GetAllGroups")]
        public IActionResult GetAllGroups()
        {
            try
            {
                IEnumerable<Group> group = _groupService.GetAllGroups();
                return Ok(group);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}

using ChocoLink.API.ViewModels;
using ChocoLink.Domain.Entity;
using ChocoLink.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChocoLink.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
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

                    var add = new Group
                    {
                        Photo = photoBytes,
                        GroupName = group.GroupName,
                        MaxParticipants = group.MaxParticipants,
                        Value = group.Value,
                        DateDiscover = group.DateDiscover,
                        Description = group.Description
                    };

                    _groupService.AddGroup(add);

                    var newGroupUser = new GroupUser
                    {
                        GroupID = add.GroupID,
                        UserID = group.UserID,
                        PerfilID = 1
                    };
                    _groupService.AddParticipant(newGroupUser);

                    return Ok("Grupo adicionado com sucesso.");
                }
                return BadRequest("Dados inválidos.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost("DeleteGroup")]
        public async Task<IActionResult> DeleteGroup([FromForm] int groupID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _groupService.DeleteGroup(groupID);
                    return Ok("Grupo excluido com sucesso.");
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
                var groups = _groupService.GetAllGroups();
                return Ok(groups);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        //[HttpPost("AddParticipant")]
        //public IActionResult AddParticipant([FromForm] NewGroupUserViewModel newGroupUser)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var (currentParticipants, maxParticipants) = _groupService.GetParticipantCount(newGroupUser.GroupID);

        //            if (currentParticipants >= maxParticipants)
        //            {
        //                return BadRequest($"O grupo atingiu o número máximo de participantes ({maxParticipants}).");
        //            }
        //            var add = new GroupUser
        //            {
        //                GroupID = newGroupUser.GroupID,
        //                UserID = newGroupUser.UserID
        //            };

        //            _groupService.AddParticipant(add);
        //            return Ok("Participante adicionado com sucesso.");
        //        }

        //        var errors = ModelState.Values.SelectMany(v => v.Errors)
        //                                      .Select(e => e.ErrorMessage)
        //                                      .ToList();
        //        return BadRequest(errors);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");

        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine($"Exceção Interna: {ex.InnerException.Message}");
        //        }

        //        return BadRequest("Ocorreu um erro ao adicionar participante.");
        //    }
        //}

        [HttpGet("GetParticipantCount/{groupId}")]
        public IActionResult GetParticipantCount(int groupId)
        {
            try
            {
                var (currentParticipants, maxParticipants) = _groupService.GetParticipantCount(groupId);
                var result = $"{currentParticipants}/{maxParticipants}";
                return Ok(result);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}

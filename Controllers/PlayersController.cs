using System.Collections.Generic;
using Saitynai_REST_L01.Data;
using Saitynai_REST_L01.Dtos;
using Saitynai_REST_L01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Saitynai_REST_L01.Services;
using Saitynai_REST_L01.Entities;

namespace Saitynai_REST_L01.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepo _repository;
        private readonly ICommanderRepo _repositoryC;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerRepo repository, IMapper mapper, ICommanderRepo repo)
        {   
            _repository = repository;
            _mapper = mapper;
            _repositoryC = repo;
        }

        //private readonly MockPlayerRepo _repository = new MockPlayerRepo();
        //GET api/Players
        [HttpGet]
        public ActionResult <IEnumerable<PlayerReadDto>> GetAllPlayers()
        {
            var PlayerItems = _repository.GetAllPlayers();

            return Ok(_mapper.Map<IEnumerable<PlayerReadDto>>(PlayerItems));
        }

        
        //GET api/Players/{id}
        //[Authorization]
        [HttpGet("{id}/command")]
        public ActionResult <PlayerReadDto> GetCommandByPlayerId(int id)
        {
            var PlayerItem = _repository.GetPlayerById(id);
            if (PlayerItem != null)
            {
                var CommandItem = _repositoryC.GetCommandById(PlayerItem.command_id);
                if (CommandItem != null)
                {
                    return Ok(_mapper.Map<CommandReadDto>(CommandItem));
                }
                
            }
            return NotFound();          
        }


        
        [HttpGet("{id}", Name="GetPlayerById")]
        //[Authorize(Roles = "Admin, User")]
        public ActionResult <PlayerReadDto> GetPlayerById(int id)
        {
            var PlayerItem = _repository.GetPlayerById(id);

            if (PlayerItem != null)
            {

                return Ok(_mapper.Map<PlayerReadDto>(PlayerItem));
            }
            return NotFound();          
        }

        //POST api/Players 
        [HttpPost]
        public ActionResult <PlayerReadDto> CreatePlayer(PlayerCreateDto PlayerCreateDto)
        {
            var PlayerModel = _mapper.Map<Player>(PlayerCreateDto);
            _repository.CreatePlayer(PlayerModel);
            _repository.SaveChanges();

            var PlayerReadDto = _mapper.Map<PlayerReadDto>(PlayerModel);

            return CreatedAtRoute(nameof(GetPlayerById), new {Id = PlayerReadDto.Id}, PlayerReadDto);

        }

        //PUT api/Players/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePlayer(int id, PlayerUpdateDto PlayerUpdateDto)
        {
            var PlayerModelFromRepo = _repository.GetPlayerById(id);
            if(PlayerModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(PlayerUpdateDto, PlayerModelFromRepo);

            _repository.UpdatePlayer(PlayerModelFromRepo);

            _repository.SaveChanges();

            return NoContent();

        }
        
        // pavyzdys patch
        // [
        //     {
        //         "op": "replace",
        //         "path": "/howto",
        //         "value": "ikelimas su PATCH"
        //     }
        // ]
        //PATCH api/Players/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialPlayerUpdate(int id, JsonPatchDocument<PlayerUpdateDto> patchDoc)
        {
            var PlayerModelFromRepo = _repository.GetPlayerById(id);
            if(PlayerModelFromRepo == null)
            {
                return NotFound();
            }

            var PlayerToPatch = _mapper.Map<PlayerUpdateDto>(PlayerModelFromRepo);
            patchDoc.ApplyTo(PlayerToPatch, ModelState);

            if(!TryValidateModel(PlayerToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(PlayerToPatch, PlayerModelFromRepo);

            _repository.UpdatePlayer(PlayerModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }



        //DELETE api/Players/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = Role.Admin)]
        public ActionResult DeletePlayer(int id)
        {
            var PlayerModelFromRepo = _repository.GetPlayerById(id);
            if(PlayerModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeletePlayer(PlayerModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
    }
}
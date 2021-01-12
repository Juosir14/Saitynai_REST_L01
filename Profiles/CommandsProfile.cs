using AutoMapper;
using Saitynai_REST_L01.Dtos;
using Saitynai_REST_L01.Models;

namespace Saitynai_REST_L01.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> Target
            CreateMap<Command, CommandReadDto>(); 
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
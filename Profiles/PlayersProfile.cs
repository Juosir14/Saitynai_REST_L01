using AutoMapper;
using Saitynai_REST_L01.Dtos;
using Saitynai_REST_L01.Models;

namespace Saitynai_REST_L01.Profiles
{
    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            CreateMap<Player, PlayerReadDto>(); 
            CreateMap<PlayerCreateDto, Player>();
            CreateMap<PlayerUpdateDto, Player>();
            CreateMap<Player, PlayerUpdateDto>();
        }   
    }
}
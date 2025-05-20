using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;

namespace ComicVerse.Application.Mappings
{
    public class EquipeProfile : Profile
    {
        public EquipeProfile()
        {
            CreateMap<Equipe, EquipeDTO>();
            CreateMap<CreateEquipeDTO, Equipe>();
            CreateMap<UpdateEquipeDTO, Equipe>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
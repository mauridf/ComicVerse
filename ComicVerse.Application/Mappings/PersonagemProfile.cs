using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;

namespace ComicVerse.Application.Mappings
{
    public class PersonagemProfile : Profile
    {
        public PersonagemProfile()
        {
            CreateMap<Personagem, PersonagemDTO>();
            CreateMap<CreatePersonagemDTO, Personagem>();
            CreateMap<UpdatePersonagemDTO, Personagem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
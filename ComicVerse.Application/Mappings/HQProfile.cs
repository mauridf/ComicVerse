using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;

namespace ComicVerse.Application.Mappings
{
    public class HQProfile : Profile
    {
        public HQProfile()
        {
            CreateMap<HQ, HQDTO>()
                .ForMember(dest => dest.Editoras, opt => opt.MapFrom(src => src.Editoras.Select(he => he.Editora)))
                .ForMember(dest => dest.Personagens, opt => opt.MapFrom(src => src.Personagens.Select(hp => hp.Personagem)))
                .ForMember(dest => dest.Equipes, opt => opt.MapFrom(src => src.Equipes.Select(he => he.Equipe)));

            CreateMap<CreateHQDTO, HQ>()
                .ForMember(dest => dest.Editoras, opt => opt.Ignore())
                .ForMember(dest => dest.Personagens, opt => opt.Ignore())
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());

            CreateMap<UpdateHQDTO, HQ>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Editoras, opt => opt.Ignore())
                .ForMember(dest => dest.Personagens, opt => opt.Ignore())
                .ForMember(dest => dest.Equipes, opt => opt.Ignore());
        }
    }
}
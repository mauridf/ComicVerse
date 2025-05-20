using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;

namespace ComicVerse.Application.Mappings
{
    public class EditoraProfile : Profile
    {
        public EditoraProfile()
        {
            CreateMap<Editora, EditoraDTO>();
            CreateMap<CreateEditoraDTO, Editora>();
            CreateMap<UpdateEditoraDTO, Editora>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
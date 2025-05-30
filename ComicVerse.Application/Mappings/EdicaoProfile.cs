﻿using AutoMapper;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;

namespace ComicVerse.Application.Mappings
{
    public class EdicaoProfile : Profile
    {
        public EdicaoProfile()
        {
            CreateMap<Edicao, EdicaoDTO>();

            CreateMap<CreateEdicaoDTO, Edicao>()
                .ForMember(dest => dest.DataLancamento, opt => opt.MapFrom<DateTime?>(src =>
                    src.DataLancamento.HasValue ?
                    DateTime.SpecifyKind(src.DataLancamento.Value, DateTimeKind.Utc) :
                    null));

            CreateMap<UpdateEdicaoDTO, Edicao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HQId, opt => opt.Ignore());
        }
    }
}
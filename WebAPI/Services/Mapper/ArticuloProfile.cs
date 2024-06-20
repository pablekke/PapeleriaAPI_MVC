using AutoMapper;
using Domain.Modelos;
using Domain.DTOs;

namespace Services
{
    internal class ArticuloProfile : Profile
    {
        public ArticuloProfile()
        {
            CreateMap<ArticuloDTO, Articulo>();
            CreateMap<Articulo, ArticuloDTO>();
        }
    }
}
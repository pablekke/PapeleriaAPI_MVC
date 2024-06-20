using AutoMapper;
using Domain.Modelos;
using Domain.DTOs;

namespace Services
{
    internal class EncargadoProfile : Profile
    {
        public EncargadoProfile()
        {
            CreateMap<EncargadoDTO, Encargado>();
            CreateMap<Encargado, EncargadoDTO>();
        }
    }
}
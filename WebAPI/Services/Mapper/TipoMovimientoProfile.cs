using AutoMapper;
using Domain.DTOs;
using Domain.Modelos;

namespace Services.Mapper
{
    internal class TipoMovimientoProfile : Profile
    {
        public TipoMovimientoProfile()
        {
            CreateMap<TipoMovimientoDTO, TipoMovimiento>();
            CreateMap<TipoMovimiento, TipoMovimientoDTO>();
        }
    }
}

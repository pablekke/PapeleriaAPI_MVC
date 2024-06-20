using AutoMapper;
using Domain.Modelos;
using Domain.DTOs;

namespace Services
{
    internal class StockArticuloProfile : Profile
    {
        public StockArticuloProfile()
        {
            CreateMap<StockArticuloDTO, StockArticulo>();
            CreateMap<StockArticulo, StockArticuloDTO>();
        }
    }
}
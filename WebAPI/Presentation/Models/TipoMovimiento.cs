using Domain.DTOs;

namespace Presentation
{
    public class TipoMovimiento
    {
        public string Nombre { get; set; }
        public bool EsAumento { get; set; }

        public TipoMovimientoDTO ToDTO()
        {
            return new TipoMovimientoDTO
            {
                TipoMovimientoId = 0,
                Nombre = Nombre,
                EsAumento = EsAumento
            };
        }
    }
}
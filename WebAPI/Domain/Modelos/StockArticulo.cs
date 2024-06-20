using Domain.Interfaces;
using Dominio.Utiles;

namespace Domain.Modelos
{
    public class StockArticulo : IValidable, ICopiable<StockArticulo>
    {
        public int MovimientoId { get; set; }
        public int ArticuloId { get; set; }
        public int TipoMovimientoId { get; set; }
        public Articulo Articulo { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public string EmailEncargado { get; set; }
        public int CantidadMovida { get; set; }
        public DateTime Fecha { get; set; }
        public static int Tope { get; set; } = 150;

        public void Copiar(StockArticulo movimiento)
        {
            ArticuloId = movimiento.ArticuloId;
            TipoMovimientoId = movimiento.TipoMovimientoId;
            EmailEncargado = movimiento.EmailEncargado;
            Fecha = movimiento.Fecha;
            CantidadMovida = movimiento.CantidadMovida;
        }

        public void Validar()
        {
            Utiles.ExcepcionSiNumeroNegativoOCero(CantidadMovida, "La cantidad debe ser mayor 0");
            Utiles.ExcepcionSiEmailInvalido(EmailEncargado, "Email inválido");
            Utiles.ExcepcionSiFechaFutura(Fecha, "La fecha no debe ser futura");
            if (CantidadMovida >Tope)
            {
                throw new ArgumentException($"La cantidad no debe superar el tope actual de máximo {Tope} unidades.");
            }
        }
    }
}
using DataAccess;
using AutoMapper;
using Domain.DTOs;
using Domain.Modelos;

namespace Services
{
    public class ServicioEncargado : Servicio<Encargado, EncargadoDTO>, IServicioEncargado
    {
        private new readonly IRepositorioEncargado _repositorio;
        public ServicioEncargado(IMapper mapeador, IRepositorioEncargado repositorio) : base(mapeador, repositorio)
        {
            _repositorio = repositorio;
        }

        public bool ExisteEmail(string email)
        {
            return _repositorio.ExisteEmail(email);
        }

        public EncargadoDTO? ExisteEncargado(string email, string contraseña)
        {
            var encargado = _repositorio.ExisteEncargado(email, contraseña);
            return _mapeador.Map<EncargadoDTO>(encargado);
        }

        #region Excepciones y controles
        protected override void ControlesAntesDeCrear(Encargado encargado)
        {
            ExcepcionSiExisteEmail(encargado.Email);
            //encargado.Contraseña = BCrypt.Net.BCrypt.HashPassword(encargado.Contraseña);
        }
        protected override void ControlesAntesDeActualizar(int id, EncargadoDTO dto)
        {
            //si el nuevo email es distinto del actual chequea que no lo tenga otro encargado ya
            var modelo = GetPorId(id);
            if (modelo.Email != dto.Email)
            {
                ExcepcionSiExisteEmail(dto.Email);
            }
        }
        private void ExcepcionSiExisteEmail(string email)
        {
            if (ExisteEmail(email))
            {
                throw new Exception("El email ya está en uso.");
            }
        }
        #endregion
    }
}
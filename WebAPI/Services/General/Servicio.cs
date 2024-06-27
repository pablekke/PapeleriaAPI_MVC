using DataAccess;
using AutoMapper;
using Domain.Interfaces;

namespace Services
{
    public class Servicio<Modelo,DTO> : IServicio<DTO>
        where Modelo : ICopiable<Modelo>, IValidable
    {
        protected IRepositorio<Modelo> _repositorio;
        protected IMapper _mapeador;

        public Servicio(IMapper mapeador, IRepositorio<Modelo> repositorio)
        {
            _repositorio = repositorio;
            _mapeador = mapeador;
        }

        public virtual void Crear(DTO dto)
        {
            ExcepcionSiClaseNula(dto);
            Modelo modelo = _mapeador.Map<Modelo>(dto);
            modelo.Validar();
            ControlesAntesDeCrear(modelo);
            _repositorio.Crear(modelo);
        }

        public void Borrar(int id)
        {
            ExcepcionSiIdInvalido(id);
            _repositorio.Borrar(id);
        }

        public void Actualizar(int id, DTO dto)
        {
            ExcepcionSiClaseNula(dto);
            ControlesAntesDeActualizar(id, dto);

            var original = _repositorio.GetPorId(id);
            var actualizado = _mapeador.Map<Modelo>(dto);
            actualizado.Validar();

            original.Copiar(actualizado);
            _repositorio.Actualizar(original);
        }
        public List<DTO> GetAll()
        {
            var entidades = _repositorio.GetAll();
            return _mapeador.Map<List<DTO>>(entidades);
        }

        public DTO GetPorId(int id)
        {
            ExcepcionSiIdInvalido(id);
            var entidad = _repositorio.GetPorId(id);
            ExcepcionSiObjetoNoExiste(entidad);
            return _mapeador.Map<DTO>(entidad);
        }

        #region  Excepciones y controles
        protected virtual void ControlesAntesDeCrear(Modelo dto)
        {
        }
        protected virtual void ControlesAntesDeActualizar(int id, DTO dto)
        {
        }
        protected void ExcepcionSiClaseNula(DTO clase)
        {
            if (clase == null)
            {
                throw new ArgumentNullException("Clase Inválida.");
            }
        }
        protected void ExcepcionSiIdInvalido(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Id inválido.");
            }
        }
        protected void ExcepcionSiObjetoNoExiste(object o)
        {
            if (o == null)
            {
                throw new InvalidOperationException("No existe un objeto con ese Id.");
            }
        }
        #endregion
    }
}
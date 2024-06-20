using Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RepositorioEncargado : Repositorio<Encargado>, IRepositorioEncargado
    {
        private readonly DbSet<Encargado> Encargados;
        public RepositorioEncargado(DbContext contexto) : base(contexto) { 
            Encargados = _contexto.Set<Encargado>();
        }
        public bool ExisteEmail(string email)
        {
            return Encargados.Any(u => u.Email == email);
        }
        public Encargado? ExisteEncargado(string email, string contraseña)
        {
            var encargado = Encargados.FirstOrDefault(u => u.Email == email);
            //if (encargado != null && contraseñaValida(contraseña, encargado.Contraseña))
            //{
            //    return encargado;
            //}
            if (encargado != null && contraseña == encargado.Contraseña)
            {
                return encargado;
            }
            return null;
        }
        private bool contraseñaValida(string cLocal, string cBase)
        {
            return BCrypt.Net.BCrypt.Verify(cLocal, cBase);
        }
    }
}
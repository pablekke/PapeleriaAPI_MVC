namespace Application
{
    public interface IServicioLogin<Credenciales, String>
    {
        String Login(Credenciales c);
    }
}
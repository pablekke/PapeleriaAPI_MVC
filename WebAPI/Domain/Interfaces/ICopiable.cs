namespace Domain.Interfaces
{
    public interface ICopiable<T>
    {
        void Copiar(T modelo);
    }
}
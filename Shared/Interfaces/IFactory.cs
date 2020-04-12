namespace Shared.Interfaces
{
    public interface IFactory
    {
        T GetService<T>();
    }
}

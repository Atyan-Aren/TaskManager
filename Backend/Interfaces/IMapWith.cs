namespace TaskManager.Interfaces
{
    public interface IMapWith<T> where T : class
    {
        T Map();
    }
}

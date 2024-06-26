namespace Users.Application
{
    public interface IUserCache<TItem> where TItem : class
    {
        void Clear();
        void DeleteItem<TRequest>(TRequest request);
        void DeleteItem<TRequest>(TRequest request, string secondKey);
        void Set<TRequest>(TRequest request, string secondKey, TItem item);
        void Set<TRequest>(TRequest request, TItem item);
        bool TryGetValue<TRequest>(TRequest request, out TItem? item);
        bool TryGetValue<TRequest>(TRequest request, string secondKey, out TItem? item);
    }
}
using Microsoft.Extensions.Caching.Memory;

namespace ServiceCenter.Application
{
    public class ServiceCenterMemoryCache
    {
        public MemoryCache Cache { get; } = new MemoryCache(
            new MemoryCacheOptions
            {
                SizeLimit = 1024,
            });
    }
}

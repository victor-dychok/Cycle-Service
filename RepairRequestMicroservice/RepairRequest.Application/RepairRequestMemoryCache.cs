using Microsoft.Extensions.Caching.Memory;

namespace RepairRequest.Application
{
    public class RepairRequestMemoryCache
    {
        public MemoryCache Cache { get; } = new MemoryCache(
            new MemoryCacheOptions
            {
                SizeLimit = 1024,
            });
    }
}

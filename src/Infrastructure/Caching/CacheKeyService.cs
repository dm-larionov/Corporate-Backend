using WebApi.Application.Common.Caching;

namespace WebApi.Infrastructure.Caching;

public class CacheKeyService : ICacheKeyService
{
    public string GetCacheKey(string name, object id, bool includeTenantId = true)
    {
        //string tenantId = includeTenantId
        //    ? _currentTenant?.Id ?? throw new InvalidOperationException("GetCacheKey: includeTenantId set to true and no ITenantInfo available.")
        //    : "GLOBAL";
        return $"{name}-{id}";
    }
}
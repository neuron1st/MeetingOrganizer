using MeetingOrganizer.Common.Extensions;
using MeetingOrganizer.Services.Settings;
using StackExchange.Redis;

namespace MeetingOrganizer.Services.Cache;

/// <inheritdoc/>
public class CacheService : ICacheService
{
    private TimeSpan defaultLifetime = TimeSpan.FromMinutes(1);

    private readonly CacheSettings settings;
    private readonly IDatabase cacheDb;

    private static string redisUri;
    private static ConnectionMultiplexer Connection => lazyConnection.Value;
    private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new(() => ConnectionMultiplexer.Connect(redisUri));

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheService"/> class.
    /// </summary>
    /// <param name="settings">The cache settings.</param>
    public CacheService(CacheSettings settings)
    {
        this.settings = settings;

        redisUri = this.settings.Uri;
        defaultLifetime = TimeSpan.FromMinutes(this.settings.CacheLifeTime);

        cacheDb = Connection.GetDatabase();
    }

    /// <inheritdoc/>
    public string KeyGenerate()
    {
        return Guid.NewGuid().Shrink();
    }

    /// <inheritdoc/>
    public async Task<bool> Put<T>(string key, T data, TimeSpan? storeTime = null)
    {
        return await cacheDb.StringSetAsync(key, data.ToJsonString(), storeTime ?? defaultLifetime);
    }

    /// <inheritdoc/>
    public async Task SetStoreTime(string key, TimeSpan? storeTime = null)
    {
        await cacheDb.KeyExpireAsync(key, storeTime ?? defaultLifetime);
    }

    /// <inheritdoc/>
    public async Task<T> Get<T>(string key, bool resetLifeTime = true)
    {
        try
        {
            string cachedData = await cacheDb.StringGetAsync(key);

            if (cachedData.IsNullOrEmpty())
                return default;

            var data = cachedData.FromJsonString<T>();

            if (resetLifeTime)
                await SetStoreTime(key);

            return data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Can't get data from cache for {key}", ex);
        }
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(string key)
    {
        return await cacheDb.KeyDeleteAsync(key);
    }
}

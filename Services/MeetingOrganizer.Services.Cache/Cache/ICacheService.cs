namespace MeetingOrganizer.Services.Cache;

using System;
using System.Threading.Tasks;

/// <summary>
/// Represents a service for caching data.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Generates a key for caching purposes.
    /// </summary>
    /// <returns>A generated key.</returns>
    string KeyGenerate();

    /// <summary>
    /// Puts data into the cache.
    /// </summary>
    /// <typeparam name="T">The type of data to cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="data">The data to cache.</param>
    /// <param name="storeTime">The optional time to store the data in the cache.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the operation succeeded.</returns>
    Task<bool> Put<T>(string key, T data, TimeSpan? storeTime = null);

    /// <summary>
    /// Sets the store time for a cached item.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="storeTime">The optional time to store the data in the cache.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetStoreTime(string key, TimeSpan? storeTime = null);

    /// <summary>
    /// Gets data from the cache.
    /// </summary>
    /// <typeparam name="T">The type of data to get from the cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="resetLifeTime">Specifies whether to reset the cache item's life time.</param>
    /// <returns>A task representing the asynchronous operation, returning the cached data if found.</returns>
    Task<T> Get<T>(string key, bool resetLifeTime = true);

    /// <summary>
    /// Deletes data from the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <returns>A task representing the asynchronous operation, returning true if the operation succeeded.</returns>
    Task<bool> Delete(string key);
}

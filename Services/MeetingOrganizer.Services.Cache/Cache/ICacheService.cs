namespace MeetingOrganizer.Services.Cache;

public interface ICacheService
{
    string KeyGenerate();

    Task<bool> Put<T>(string key, T data, TimeSpan? storeTime = null);

    Task SetStoreTime(string key, TimeSpan? storeTime = null);

    Task<T> Get<T>(string key, bool resetLifeTime = true);

    Task<bool> Delete(string key);
}

namespace JPT.UseCases.Abstractions.WebStorages;

public interface ICookieService
{
    void Set(string key, string value, DateTimeOffset expiresAt);

    string? Get(string key);

    void Delete(string key);
}
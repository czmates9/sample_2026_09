
using TeklaModelInspector.Models;

namespace TeklaModelInspector.Services;

public interface ITeklaService
{
    bool IsConnected { get; }

    string ModelName { get; }

    Task<IReadOnlyCollection<TeklaPart>> GetPartsAsync();
}

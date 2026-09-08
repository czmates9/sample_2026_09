using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeklaModelInspector.Models;

namespace TeklaModelInspector.Services;

public interface ITeklaService
{
    bool IsConnected { get; }

    string ModelName { get; }

    Task<IReadOnlyCollection<TeklaPart>> GetPartsAsync();
}

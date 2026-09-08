using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeklaModelInspector.Models;

namespace TeklaModelInspector.Services;

public class MockTeklaService : ITeklaService
{
    public bool IsConnected => true;

    public string ModelName => "Demo výrobní hala – Brno";

    public async Task<IReadOnlyCollection<TeklaPart>> GetPartsAsync()
    {
        // Simulace komunikace s externí aplikací.
        await Task.Delay(700);

        return new List<TeklaPart>
        {
            new()
            {
                Id = 1001,
                Name = "Sloup S1",
                Profile = "HEA300",
                Material = "S355",
                ObjectType = "Beam"
            },
            new()
            {
                Id = 1002,
                Name = "Nosník N1",
                Profile = "IPE240",
                Material = "S355",
                ObjectType = "Beam"
            },
            new()
            {
                Id = 1003,
                Name = "Patní deska P1",
                Profile = "PL20",
                Material = "S235",
                ObjectType = "ContourPlate"
            }
        };
    }
}

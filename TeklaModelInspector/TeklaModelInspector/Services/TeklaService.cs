using Tekla.Structures.Model;
using TeklaModelInspector.Models;

namespace TeklaModelInspector.Services;

public class TeklaService : ITeklaService
{
    private readonly Model _model = new();

    public bool IsConnected => _model.GetConnectionStatus();

    public string ModelName
    {
        get
        {
            if (!IsConnected)
                return string.Empty;

            return _model.GetInfo().ModelName;
        }
    }

    public System.Threading.Tasks.Task<IReadOnlyCollection<TeklaPart>> GetPartsAsync()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException(
                "Tekla Structures není spuštěná nebo není otevřený model.");
        }

        var parts = new List<TeklaPart>();

        var selector = _model.GetModelObjectSelector();

        var modelObjects = selector.GetAllObjectsWithType(
            ModelObject.ModelObjectEnum.BEAM);

        while (modelObjects.MoveNext())
        {
            if (modelObjects.Current is not Beam beam)
                continue;

            parts.Add(new TeklaPart
            {
                Id = beam.Identifier.ID,
                Name = beam.Name,
                Profile = beam.Profile.ProfileString,
                Material = beam.Material.MaterialString,
                ObjectType = beam.GetType().Name
            });
        }

        return System.Threading.Tasks.Task.FromResult<IReadOnlyCollection<TeklaPart>>(parts);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeklaModelInspector.Models;

public class TeklaPart
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Profile { get; set; } = string.Empty;

    public string Material { get; set; } = string.Empty;

    public string ObjectType { get; set; } = string.Empty;
}
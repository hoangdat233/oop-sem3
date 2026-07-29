using Itmo.ObjectOrientedProgramming.Lab3.Models;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class CatalogCreatureFactory : ICreatureFactory
{
    private readonly CreatureCatalog _catalog;
    private readonly string _creatureName;

    public CatalogCreatureFactory(CreatureCatalog catalog, string creatureName)
    {
        _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
        _creatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
    }

    public BaseCreature CreateCreature()
    {
        return _catalog.CreateCreature(_creatureName);
    }
}
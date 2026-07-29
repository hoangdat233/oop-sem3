using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

/// <summary>
/// Catalog of available creatures.
/// Implements Abstract Factory pattern - provides interface for creating families of related objects.
/// </summary>
public class CreatureCatalog
{
    // Dictionary acts as registry for factory methods
    private readonly Dictionary<string, Func<BaseCreature>> _creatureFactories = new Dictionary<string, Func<BaseCreature>>();

    public CreatureCatalog()
    {
        // Register default creatures
        RegisterCreature("BattleAnalyst", () => new BattleAnalyst());
        RegisterCreature("ViciousFighter", () => new ViciousFighter());
        RegisterCreature("MimicChest", () => new MimicChest());
        RegisterCreature("ImmortalHorror", () => new ImmortalHorror());
        RegisterCreature("AmuletMaster", () => new AmuletMaster());
    }

    public void RegisterCreature(string name, Func<BaseCreature> factory)
    {
        _creatureFactories[name] = factory;
    }

    public BaseCreature CreateCreature(string name)
    {
        if (_creatureFactories.ContainsKey(name))
        {
            return _creatureFactories[name]();
        }

        throw new ArgumentException($"Creature '{name}' not found in catalog");
    }

    public ReadOnlyCollection<string> GetAvailableCreatures()
    {
        return _creatureFactories.Keys.ToList().AsReadOnly();
    }
}
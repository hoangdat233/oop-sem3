using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

/// <summary>
/// Represents a player's table containing creatures.
/// Implements Composite pattern - manages collection of creatures as a single unit.
/// Adapter pattern - adapts creature collection to IBattleParticipant interface.
/// </summary>
public class PlayerTable : IBattleParticipant
{
    // Max creatures allowed per player
    private const int MaxCreaturesAllowed = 7;
    private readonly List<BaseCreature> _creatures = new List<BaseCreature>();

    public IReadOnlyList<BaseCreature> Creatures => _creatures.AsReadOnly();

    public string PlayerName { get; }

    public PlayerTable(string playerName)
    {
        PlayerName = playerName;
    }

    public bool AddCreature(BaseCreature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (_creatures.Count >= MaxCreaturesAllowed)
            return false;

        _creatures.Add(creature);
        return true;
    }

    public ReadOnlyCollection<BaseCreature> GetAttackingCreatures()
    {
        return _creatures.Where(c => c.CanAttack).ToList().AsReadOnly();
    }

    public ReadOnlyCollection<BaseCreature> GetAttackableCreatures()
    {
        return _creatures.Where(c => c.IsAlive).ToList().AsReadOnly();
    }

    public bool HasAliveCreatures()
    {
        return _creatures.Any(c => c.IsAlive);
    }

    public void ResetCreatures()
    {
        foreach (BaseCreature creature in _creatures)
        {
            creature.ResetToBaseStats();
        }
    }
}
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

/// <summary>
/// Factory interface for creating creatures.
/// Implements Factory Method pattern - defines interface for object creation.
/// </summary>
public interface ICreatureFactory
{
    BaseCreature CreateCreature();
}
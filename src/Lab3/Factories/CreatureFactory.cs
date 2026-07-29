using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Factories;

public class CreatureFactory<T> : ICreatureFactory where T : BaseCreature, new()
{
    public BaseCreature CreateCreature()
    {
        return new T();
    }
}
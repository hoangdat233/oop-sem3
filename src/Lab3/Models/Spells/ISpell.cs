using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;

public interface ISpell
{
    void Apply(BaseCreature creature);
}
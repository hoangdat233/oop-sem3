using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;

public class MagicMirror : ISpell
{
    public void Apply(BaseCreature creature)
    {
        (creature.BaseAttack, creature.BaseHealth) = (creature.BaseHealth, creature.BaseAttack);
        (creature.CurrentAttack, creature.CurrentHealth) = (creature.CurrentHealth, creature.CurrentAttack);
    }
}
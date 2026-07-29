using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;

public class EndurancePotion : ISpell
{
    public void Apply(BaseCreature creature)
    {
        creature.BaseHealth += 5;
        creature.CurrentHealth += 5;
    }
}
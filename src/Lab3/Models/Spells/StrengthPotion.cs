using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;

public class StrengthPotion : ISpell
{
    public void Apply(BaseCreature creature)
    {
        creature.BaseAttack += 5;
        creature.CurrentAttack += 5;
    }
}
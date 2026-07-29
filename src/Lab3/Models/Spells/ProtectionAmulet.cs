using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;

public class ProtectionAmulet : ISpell
{
    public void Apply(BaseCreature creature)
    {
        creature.ApplyExternalModifier(new MagicShield());
    }
}
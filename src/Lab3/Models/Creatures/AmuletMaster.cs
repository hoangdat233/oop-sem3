using Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

public class AmuletMaster : BaseCreature
{
    public AmuletMaster() : base(5, 2)
    {
        AddModifier(new MagicShield());
        AddModifier(new AttackMastery());
    }
}
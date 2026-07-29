using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;

public class MagicShield : IModifier
{
    private bool _isActive = true;

    public void BeforeAttack(BaseCreature attacker, IAttackable target) { }

    public void AfterAttack(BaseCreature attacker, IAttackable target) { }

    public int ModifyDamage(BaseCreature creature, int damage)
    {
        if (_isActive && damage > 0)
        {
            _isActive = false;
            creature.RemoveModifier(this);
            return 0;
        }

        return damage;
    }
}
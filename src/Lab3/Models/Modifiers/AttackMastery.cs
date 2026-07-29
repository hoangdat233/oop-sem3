using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;

public class AttackMastery : IModifier
{
    public void BeforeAttack(BaseCreature attacker, IAttackable target) { }

    public void AfterAttack(BaseCreature attacker, IAttackable target)
    {
        if (attacker.IsAlive && attacker.CanAttack && target.IsAlive)
        {
            target.TakeDamage(attacker.CurrentAttack);
        }
    }

    public int ModifyDamage(BaseCreature creature, int damage)
    {
        return damage;
    }
}
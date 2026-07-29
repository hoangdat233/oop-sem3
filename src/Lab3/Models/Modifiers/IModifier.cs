using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;

/// <summary>
/// Interface for creature modifiers.
/// Implements Decorator pattern - allows dynamic addition of behaviors to creatures.
/// Note: For access control, see CreatureProxy class (Proxy pattern).
/// </summary>
public interface IModifier
{
    void BeforeAttack(BaseCreature attacker, IAttackable target);

    void AfterAttack(BaseCreature attacker, IAttackable target);

    int ModifyDamage(BaseCreature creature, int damage);
}
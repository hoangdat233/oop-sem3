using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

/// <summary>
/// Proxy pattern - wraps BaseCreature and controls access to its methods.
/// Provides logging, validation, and access control.
/// </summary>
public class CreatureProxy : IAttacker, IAttackable
{
    private BaseCreature RealCreature { get; }

    public CreatureProxy(BaseCreature realCreature)
    {
        RealCreature = realCreature ?? throw new ArgumentNullException(nameof(realCreature));
    }

    public bool IsAttackAllowed { get; set; } = true;

    public void Attack(IAttackable target)
    {
        if (!IsAttackAllowed)
        {
            return;
        }

        if (target == null || !target.IsAlive)
        {
            return;
        }

        // Delegate to real object
        RealCreature.Attack(target);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        RealCreature.TakeDamage(damage);
    }

    public int AttackValue => RealCreature.AttackValue;

    public bool CanAttack => RealCreature.CanAttack && IsAttackAllowed;

    public int Health => RealCreature.Health;

    public bool IsAlive => RealCreature.IsAlive;
}

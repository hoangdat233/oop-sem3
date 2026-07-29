using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

public class MimicChest : BaseCreature
{
    public MimicChest() : base(1, 1) { }

    public override void Attack(IAttackable target)
    {
        if (target is BaseCreature targetCreature)
        {
            CurrentAttack = Math.Max(CurrentAttack, targetCreature.CurrentAttack);
            CurrentHealth = Math.Max(CurrentHealth, targetCreature.CurrentHealth);
        }

        base.Attack(target);
    }
}
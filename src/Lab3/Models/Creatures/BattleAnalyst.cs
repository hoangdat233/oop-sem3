using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

public class BattleAnalyst : BaseCreature
{
    private bool _hasBoostedThisBattle;

    public BattleAnalyst() : base(2, 4)
    {
        _hasBoostedThisBattle = false;
    }

    public override void Attack(IAttackable target)
    {
        if (!_hasBoostedThisBattle)
        {
            int strategicBoost = 2;
            CurrentAttack += strategicBoost;
            _hasBoostedThisBattle = true;
        }

        base.Attack(target);
    }

    public override void ResetToBaseStats()
    {
        base.ResetToBaseStats();
        _hasBoostedThisBattle = false;
    }
}
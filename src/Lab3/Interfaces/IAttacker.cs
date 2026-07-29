namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IAttacker
{
    void Attack(IAttackable target);

    int AttackValue { get; }

    bool CanAttack { get; }
}
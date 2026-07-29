namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IAttackable
{
    void TakeDamage(int damage);

    int Health { get; }

    bool IsAlive { get; }
}
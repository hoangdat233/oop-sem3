namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

public class ViciousFighter : BaseCreature
{
    public ViciousFighter() : base(1, 6) { }

    public override void TakeDamage(int damage)
    {
        int healthBeforeDamage = CurrentHealth;

        base.TakeDamage(damage);

        if (IsAlive && healthBeforeDamage > CurrentHealth)
        {
            CurrentAttack *= 2;
        }
    }
}
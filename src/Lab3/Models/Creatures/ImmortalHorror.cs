namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

public class ImmortalHorror : BaseCreature
{
    private bool _hasRevived = false;

    public ImmortalHorror() : base(4, 4) { }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (!IsAlive && !_hasRevived)
        {
            CurrentHealth = 1;
            _hasRevived = true;
        }
    }

    public override void ResetToBaseStats()
    {
        base.ResetToBaseStats();
        _hasRevived = false;
    }
}
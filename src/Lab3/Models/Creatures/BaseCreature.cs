using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;

/// <summary>
/// Base class for all creatures in the battle system.
/// Uses Decorator pattern - modifiers can be dynamically added to change creature behavior.
/// Implements Template Method pattern - defines skeleton of attack/damage operations.
/// </summary>
public abstract class BaseCreature : IAttacker, IAttackable
{
    public int BaseAttack { get; set; }

    public int BaseHealth { get; set; }

    public int CurrentAttack { get; set; }

    public int CurrentHealth { get; set; }

    public int AttackValue => CurrentAttack;

    public int Health => CurrentHealth;

    public bool IsAlive => CurrentHealth > 0;

    public bool CanAttack => IsAlive && CurrentAttack > 0;

    // Collection of modifiers implementing Decorator pattern
    protected Collection<IModifier> Modifiers { get; } = new Collection<IModifier>();

    protected BaseCreature(int baseAttack, int baseHealth)
    {
        BaseAttack = baseAttack;
        BaseHealth = baseHealth;
        CurrentAttack = baseAttack;
        CurrentHealth = baseHealth;
    }

    public virtual void Attack(IAttackable target)
    {
        if (!CanAttack || target == null || !target.IsAlive)
            return;

        // Execute pre-attack modifiers (Decorator pattern)
        foreach (IModifier modifier in Modifiers.ToList())
        {
            modifier.BeforeAttack(this, target);
        }

        target.TakeDamage(CurrentAttack);

        // Execute post-attack modifiers (Decorator pattern)
        foreach (IModifier modifier in Modifiers.ToList())
        {
            modifier.AfterAttack(this, target);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        foreach (IModifier modifier in Modifiers.ToList())
        {
            damage = modifier.ModifyDamage(this, damage);
        }

        if (damage > 0)
        {
            CurrentHealth -= damage;
        }
    }

    // Public method for spell application (external configuration)
    public void ApplyExternalModifier(IModifier modifier)
    {
        Modifiers.Add(modifier);
    }

    public void RemoveModifier(IModifier modifier)
    {
        Modifiers.Remove(modifier);
    }

    public virtual void ResetToBaseStats()
    {
        CurrentAttack = BaseAttack;
        CurrentHealth = BaseHealth;
        Modifiers.Clear();
    }

    public override string ToString()
    {
        return $"{GetType().Name} ({CurrentAttack}/{CurrentHealth})";
    }

    // Protected - only derived classes can add modifiers during construction/configuration
    protected void AddModifier(IModifier modifier)
    {
        Modifiers.Add(modifier);
    }
}
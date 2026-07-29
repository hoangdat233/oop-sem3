using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class Lab3TestsUnit
{
    [Fact]
    public void BattleAnalyst_Attack_IncreasesAttackOnce()
    {
        var analyst = new BattleAnalyst();
        var dummy = new ViciousFighter();

        int attackBefore = analyst.CurrentAttack;
        analyst.Attack(dummy);
        int attackAfter = analyst.CurrentAttack;

        Assert.Equal(attackBefore + 2, attackAfter);
    }

    [Fact]
    public void ViciousFighter_TakeDamage_DoublesAttackOnNonFatal()
    {
        var fighter = new ViciousFighter();
        int attackBefore = fighter.CurrentAttack;
        fighter.TakeDamage(1);
        Assert.Equal(attackBefore * 2, fighter.CurrentAttack);
    }

    [Fact]
    public void ImmortalHorror_RevivesOnce()
    {
        var horror = new ImmortalHorror();
        horror.TakeDamage(10);
        Assert.Equal(1, horror.CurrentHealth);
        horror.TakeDamage(1);
        Assert.Equal(0, horror.CurrentHealth);
    }

    [Fact]
    public void MagicShield_BlocksFirstDamage()
    {
        var creature = new ViciousFighter();
        creature.ApplyExternalModifier(new MagicShield());
        creature.TakeDamage(10);
        Assert.Equal(creature.BaseHealth, creature.CurrentHealth);
        creature.TakeDamage(5);
        Assert.Equal(creature.BaseHealth - 5, creature.CurrentHealth);
    }

    [Fact]
    public void StrengthPotion_IncreasesAttack()
    {
        var creature = new ViciousFighter();
        var spell = new StrengthPotion();
        int before = creature.BaseAttack;
        spell.Apply(creature);
        Assert.Equal(before + 5, creature.BaseAttack);
    }

    [Fact]
    public void EndurancePotion_IncreasesHealth()
    {
        var creature = new ViciousFighter();
        var spell = new EndurancePotion();
        int before = creature.BaseHealth;
        spell.Apply(creature);
        Assert.Equal(before + 5, creature.BaseHealth);
    }

    [Fact]
    public void MagicMirror_SwapsAttackAndHealth()
    {
        var creature = new ViciousFighter();
        creature.BaseAttack = 2;
        creature.BaseHealth = 10;
        var spell = new MagicMirror();
        spell.Apply(creature);
        Assert.Equal(10, creature.BaseAttack);
        Assert.Equal(2, creature.BaseHealth);
    }

    [Fact]
    public void MultipleModifiers_SameType_StackCorrectly()
    {
        var creature = new ViciousFighter();
        creature.ApplyExternalModifier(new MagicShield());
        creature.ApplyExternalModifier(new MagicShield());

        creature.TakeDamage(10);
        Assert.Equal(creature.BaseHealth, creature.CurrentHealth);

        creature.TakeDamage(10);
        Assert.Equal(creature.BaseHealth, creature.CurrentHealth);

        creature.TakeDamage(5);
        Assert.Equal(creature.BaseHealth - 5, creature.CurrentHealth);
    }

    [Fact]
    public void Spell_AppliedMultipleTimes_StacksCorrectly()
    {
        var creature = new ViciousFighter();
        var spell = new StrengthPotion();
        int before = creature.BaseAttack;

        spell.Apply(creature);
        spell.Apply(creature);

        Assert.Equal(before + 10, creature.BaseAttack);
    }

    [Fact]
    public void Creature_AfterReset_RestoresToBase()
    {
        var creature = new BattleAnalyst();
        var dummy = new ViciousFighter();

        int baseAttack = creature.BaseAttack;
        creature.Attack(dummy); // Boosts attack
        Assert.NotEqual(baseAttack, creature.CurrentAttack);

        creature.ResetToBaseStats();
        Assert.Equal(baseAttack, creature.CurrentAttack);
    }

    [Fact]
    public void AttackMastery_AttacksTwice_IfTargetSurvives()
    {
        var attacker = new ViciousFighter();
        attacker.BaseAttack = 1;
        attacker.CurrentAttack = 1;
        attacker.ApplyExternalModifier(new AttackMastery());

        var defender = new ViciousFighter();
        int defenderHealthBefore = defender.CurrentHealth;

        attacker.Attack(defender);

        Assert.Equal(defenderHealthBefore - 2, defender.CurrentHealth);
    }

    [Fact]
    public void MimicChest_CopiesMaxStats()
    {
        var mimic = new MimicChest();
        mimic.CurrentAttack = 1;
        mimic.CurrentHealth = 10;

        var target = new ViciousFighter();
        target.CurrentAttack = 5;
        target.CurrentHealth = 2;

        mimic.Attack(target);

        Assert.Equal(5, mimic.CurrentAttack);
        Assert.Equal(10, mimic.CurrentHealth);
    }

    [Fact]
    public void ProtectionAmulet_AddsMagicShield()
    {
        var creature = new ViciousFighter();
        var spell = new ProtectionAmulet();

        spell.Apply(creature);

        creature.TakeDamage(10);
        Assert.Equal(creature.BaseHealth, creature.CurrentHealth);
        creature.TakeDamage(5);
        Assert.Equal(creature.BaseHealth - 5, creature.CurrentHealth);
    }

    [Fact]
    public void CreatureProxy_BlocksAttack_WhenNotAllowed()
    {
        var fighter = new ViciousFighter();
        var proxy = new CreatureProxy(fighter);
        var target = new ViciousFighter();

        int targetHealthBefore = target.Health;

        proxy.IsAttackAllowed = false;
        proxy.Attack(target);

        Assert.Equal(targetHealthBefore, target.Health);
    }

    [Fact]
    public void CreatureProxy_AllowsAttack_WhenAllowed()
    {
        var fighter = new ViciousFighter();
        var proxy = new CreatureProxy(fighter);
        var target = new ViciousFighter();

        int targetHealthBefore = target.Health;

        proxy.IsAttackAllowed = true;
        proxy.Attack(target);

        Assert.True(target.Health < targetHealthBefore);
    }

    [Fact]
    public void CreatureProxy_PreventNegativeDamage()
    {
        var fighter = new ViciousFighter();
        var proxy = new CreatureProxy(fighter);

        int healthBefore = proxy.Health;

        proxy.TakeDamage(-10);

        Assert.Equal(healthBefore, proxy.Health);
    }

    [Fact]
    public void CreatureProxy_DelegatesPropertiesToRealCreature()
    {
        var analyst = new BattleAnalyst();
        var proxy = new CreatureProxy(analyst);

        Assert.Equal(analyst.AttackValue, proxy.AttackValue);
        Assert.Equal(analyst.Health, proxy.Health);
        Assert.Equal(analyst.IsAlive, proxy.IsAlive);
        Assert.Equal(analyst.CanAttack, proxy.CanAttack);
    }
}

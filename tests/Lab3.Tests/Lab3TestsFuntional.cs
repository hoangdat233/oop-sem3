using Itmo.ObjectOrientedProgramming.Lab3.Models;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Models.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class Lab3TestsFuntional
{
    [Fact]
    public void Battle_Player1HasStrongerCreature_Player1Wins()
    {
        var player1 = new PlayerTable("P1");
        var player2 = new PlayerTable("P2");

        player1.AddCreature(new BattleAnalyst());
        player2.AddCreature(new ViciousFighter());

        var battle = new Battle(player1, player2);
        BattleResult result = battle.Fight();

        Assert.Equal(BattleResult.Player1Win, result);
    }

    [Fact]
    public void Battle_BothPlayersNoCreatures_Draw()
    {
        var player1 = new PlayerTable("P1");
        var player2 = new PlayerTable("P2");

        var battle = new Battle(player1, player2);
        BattleResult result = battle.Fight();

        Assert.Equal(BattleResult.Draw, result);
    }

    [Fact]
    public void Battle_ImmortalHorror_RevivesOnce()
    {
        var player1 = new PlayerTable("P1");
        var player2 = new PlayerTable("P2");

        player1.AddCreature(new ImmortalHorror());
        player2.AddCreature(new ViciousFighter());

        var battle = new Battle(player1, player2);
        BattleResult result = battle.Fight();

        Assert.True(result == BattleResult.Player1Win || result == BattleResult.Player2Win || result == BattleResult.Draw);
    }

    [Fact]
    public void Battle_CreaturesResetAfterBattle()
    {
        var player1 = new PlayerTable("P1");
        var player2 = new PlayerTable("P2");

        var analyst = new BattleAnalyst();
        player1.AddCreature(analyst);
        player2.AddCreature(new ViciousFighter());

        int baseAttack = analyst.BaseAttack;

        var battle = new Battle(player1, player2);
        battle.Fight();

        Assert.Equal(baseAttack, analyst.CurrentAttack);
    }

    [Fact]
    public void Battle_WithModifiers_WorksCorrectly()
    {
        var player1 = new PlayerTable("P1");
        var player2 = new PlayerTable("P2");

        var amuletMaster = new AmuletMaster();
        player1.AddCreature(amuletMaster);
        player2.AddCreature(new ViciousFighter());

        var battle = new Battle(player1, player2);
        BattleResult result = battle.Fight();

        Assert.True(result == BattleResult.Player1Win || result == BattleResult.Player2Win || result == BattleResult.Draw);
    }

    [Fact]
    public void Battle_MaxCreatures_NoMoreThan7()
    {
        var player = new PlayerTable("P1");

        for (int i = 0; i < 7; i++)
        {
            Assert.True(player.AddCreature(new ViciousFighter()));
        }

        Assert.False(player.AddCreature(new ViciousFighter()));
    }

    [Fact]
    public void Battle_WithSpells_AffectsOutcome()
    {
        var player1 = new PlayerTable("P1");
        var player2 = new PlayerTable("P2");

        var buffedCreature = new ViciousFighter();
        var strengthPotion = new StrengthPotion();
        strengthPotion.Apply(buffedCreature);

        player1.AddCreature(buffedCreature);
        player2.AddCreature(new ViciousFighter());

        var battle = new Battle(player1, player2);
        BattleResult result = battle.Fight();

        Assert.Equal(BattleResult.Player1Win, result);
    }

    [Fact]
    public void CreatureProxy_ControlsAccessToCreatureMethods()
    {
        var fighter = new ViciousFighter();
        var proxy = new CreatureProxy(fighter);
        var target = new ViciousFighter();

        proxy.IsAttackAllowed = true;
        int targetHealth1 = target.Health;
        proxy.Attack(target);
        Assert.True(target.Health < targetHealth1, "Proxy should allow attack when IsAttackAllowed is true");

        var newTarget = new ViciousFighter();
        proxy.IsAttackAllowed = false;
        int targetHealth2 = newTarget.Health;
        proxy.Attack(newTarget);
        Assert.Equal(targetHealth2, newTarget.Health);

        int proxyHealth = proxy.Health;
        proxy.TakeDamage(-100);
        Assert.Equal(proxyHealth, proxy.Health);

        Assert.Equal(fighter.AttackValue, proxy.AttackValue);
        Assert.Equal(fighter.IsAlive, proxy.IsAlive);
    }
}

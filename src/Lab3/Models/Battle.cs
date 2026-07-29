using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Services;

namespace Itmo.ObjectOrientedProgramming.Lab3.Models;

/// <summary>
/// Represents a battle between two player tables.
/// </summary>
public class Battle
{
    private readonly PlayerTable _player1;
    private readonly PlayerTable _player2;
    private readonly SecureRandomSelector _randomSelector;

    public Battle(PlayerTable player1, PlayerTable player2)
    {
        _player1 = player1;
        _player2 = player2;
        _randomSelector = new SecureRandomSelector();
    }

    public BattleResult Fight()
    {
        int turn = 0;

        while (_player1.HasAliveCreatures() && _player2.HasAliveCreatures())
        {
            PlayerTable attackerTable = turn % 2 == 0 ? _player1 : _player2;
            PlayerTable defenderTable = turn % 2 == 0 ? _player2 : _player1;

            var attackers = new List<BaseCreature>(attackerTable.GetAttackingCreatures());
            var defenders = new List<BaseCreature>(defenderTable.GetAttackableCreatures());

            // Check various end conditions
            if (attackers.Count == 0 && defenders.Count == 0)
            {
                ResetPlayers();
                return BattleResult.Draw;
            }

            if (attackers.Count == 0)
            {
                turn++;
                continue;
            }

            if (defenders.Count == 0)
            {
                ResetPlayers();
                return attackerTable == _player1 ? BattleResult.Player1Win : BattleResult.Player2Win;
            }

            BaseCreature attacker = attackers[_randomSelector.SelectIndex(attackers.Count)];
            BaseCreature defender = defenders[_randomSelector.SelectIndex(defenders.Count)];

            attacker.Attack(defender);

            turn++;

            // Safety check - prevent infinite loops
            if (turn > 1000)
            {
                ResetPlayers();
                return BattleResult.Draw;
            }
        }

        BattleResult result = DetermineWinner();
        ResetPlayers();
        return result;
    }

    private BattleResult DetermineWinner()
    {
        bool player1Alive = _player1.HasAliveCreatures();
        bool player2Alive = _player2.HasAliveCreatures();

        if (!player1Alive && !player2Alive)
            return BattleResult.Draw;

        return player1Alive ? BattleResult.Player1Win : BattleResult.Player2Win;
    }

    private void ResetPlayers()
    {
        _player1.ResetCreatures();
        _player2.ResetCreatures();
    }
}

public enum BattleResult
{
    Player1Win,
    Player2Win,
    Draw,
}

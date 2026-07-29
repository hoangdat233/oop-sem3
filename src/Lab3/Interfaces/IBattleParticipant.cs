using Itmo.ObjectOrientedProgramming.Lab3.Models.Creatures;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface IBattleParticipant
{
    ReadOnlyCollection<BaseCreature> GetAttackingCreatures();

    ReadOnlyCollection<BaseCreature> GetAttackableCreatures();

    bool HasAliveCreatures();
}
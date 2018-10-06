using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Bot;
using LHGames.Helper;

internal class CombatState : State
{
    Player targetPlayer;
    IPlayer playerInfo;
    Point attackDestination;

    public override void StartStates()
    {
        throw new NotImplementedException();
    }

    public void SetAttackDestination(Player newTargetPlayer)
    {
        targetPlayer = newTargetPlayer;
        attackDestination = targetPlayer.Position;
    }

    protected override string UpdateState()
    {
        if(targetPlayer.Health > 0)
        {
            return AttackPlayer();
        }
        else
        {
            return ReturnHome();
        }
    }

    string AttackPlayer()
    {
        if (Adjacent(attackDestination))
        {
            return AIHelper.CreateMeleeAttackAction(attackDestination);
        }
        else
        {
            return GoTo(attackDestination);
        }
    }
}



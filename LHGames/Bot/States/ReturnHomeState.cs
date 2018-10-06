using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Helper;

internal class ReturnHomeState : State
{
    public override void StartStates()
    {
        
    }

    protected override string UpdateState()
    {
        if(brain.playerInfo.Position != brain.playerInfo.HouseLocation)
        {
            return brain.GetDirection(brain.playerInfo.HouseLocation);
        }
        ExitCurrentState();
        return AIHelper.CreateEmptyAction();
    }
}


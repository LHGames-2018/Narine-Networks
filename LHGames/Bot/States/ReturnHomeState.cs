using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Helper;

internal class ReturnHomeState : State
{
    public override void StartStates()
    {
        throw new NotImplementedException();
    }

    protected override string UpdateState()
    {
        return brain.GetDirection(brain.playerInfo.HouseLocation);
    }
}


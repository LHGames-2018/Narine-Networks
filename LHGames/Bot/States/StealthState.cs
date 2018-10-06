using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Bot;
using LHGames.Helper;

internal class StealthState : State
{
    Point stealthDestination;

    public override void StartStates()
    {
    }

    public void SetStealthDestination(Point newStealthDestination)
    {
        stealthDestination = newStealthDestination;
    }

    protected override string UpdateState()
    {
        if(brain.playerInfo.CarriedResources < brain.playerInfo.CarryingCapacity)
        {
            return StealthRessource();
        }
        else
        {
            return ReturnHome();
        }
    }

    string StealthRessource()
    {
        if (Adjacent(stealthDestination))
        {
            return AIHelper.CreateStealAction(stealthDestination);
        }
        else
        {
            return GoTo(stealthDestination);
        }
    }
}


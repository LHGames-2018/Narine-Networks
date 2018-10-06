using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Bot;
using LHGames.Helper;


internal class MiningState : State
{

    Point miniralPosition;

    public override void StartStates()
    {

    }

    protected override string UpdateState()
    {
        //Pas full
        if(brain.playerInfo.CarriedResources < brain.playerInfo.CarryingCapacity)
        {
            return GatherRessource();
        }
        else
        {
            return ReturnHome();
        }
    }

    string GatherRessource()
    {
        if(Adjacent(miniralPosition))
        {
            return AIHelper.CreateCollectAction(miniralPosition);
        }
        else
        {
            return GoTo(miniralPosition);
        }
    }

    
}

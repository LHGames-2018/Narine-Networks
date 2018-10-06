using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Bot;
using LHGames.Helper;


internal class MiningState : State
{

    Point miningDestination;

    public void SetMineral(Point miningDestination)
    {
        this.miningDestination = miningDestination;
    }

    public override void StartStates()
    {
    }

    protected override string UpdateState()
    {
        //Pas full
        Console.WriteLine(brain.playerInfo.CarriedResources + "/" + brain.playerInfo.CarryingCapacity);
        if (brain.playerInfo.CarriedResources < brain.playerInfo.CarryingCapacity)
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
        if(Adjacent(miningDestination))
        {
            return AIHelper.CreateCollectAction(Direction(brain.playerInfo.Position, miningDestination));
        }
        else
        {
            return GoTo(miningDestination);
        }
    }

    
}

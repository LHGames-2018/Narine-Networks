using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHGames.Helper;


internal class UpgradeState : State
{

    public UpgradeState()
    {

    }

    public override void StartStates()
    {
            
    }

    protected override string UpdateState()
    {
        int collectingLevel = brain.playerInfo.GetUpgradeLevel(UpgradeType.CollectingSpeed);
        if (brain.playerInfo.Position == brain.playerInfo.HouseLocation && collectingLevel != 5)
        {
            brain.UpgradeGear(UpgradeType.CollectingSpeed);
            return AIHelper.CreateUpgradeAction(UpgradeType.CollectingSpeed);
        }
        return GoTo(brain.playerInfo.HouseLocation);
    }
}

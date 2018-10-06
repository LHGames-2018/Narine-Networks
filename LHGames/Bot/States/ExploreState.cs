using LHGames.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


internal class ExploreState : State
{
    Point destination;


    public override void StartStates()
    {

    }

    protected override string UpdateState()
    {
        if(HouseInSight())
        {
            return "";
        }
        else
        {
            return SearchDirection(new Point(-1,0));
        }
    }

    bool HouseInSight()
    {
        return false;
    }


    string SearchDirection(Point direction)
    {
        Point position = brain.playerInfo.HouseLocation;
        for (int i = 0; i < 20; i++)
        {
            Point nextPosition = position + direction;
            if (IsOutOfMap(position))
            {
                break;
            }
            else
            {
                TileContent tile = brain.map.GetTileAt(nextPosition.X, nextPosition.Y);
                if (tile == TileContent.Wall || tile == TileContent.Resource)
                {
                    return GoTo(nextPosition);
                }

            }
            position = nextPosition;
        }

        return GoTo(position);
    }

    //string DropDownLow()
    //{
    //    return 
    //}
}

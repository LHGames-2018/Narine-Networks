using LHGames.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


internal class ExploreState : State
{
    Point destination;
    TileContent destinationType;

    public override void StartStates()
    {

    }

    protected override string UpdateState()
    {
        if(HouseInSight())
        {
            if(Adjacent(destination))
            {
                return AIHelper.CreateStealAction(Direction(brain.playerInfo.Position, destination));
            }
            else
            {
                return GoTo(destination);
            }
        }
        else
        {
            if(destination != null && Adjacent(destination))
            {
                if(destinationType == TileContent.Resource)
                    return AIHelper.CreateCollectAction(Direction(brain.playerInfo.Position, destination));
                else //(destinationType == TileContent.Wall)
                    return AIHelper.CreateMeleeAttackAction(Direction(brain.playerInfo.Position, destination));
            }
            else
            {
                return SearchDirection(new Point(-1, 0));
            }
        }
    }

    bool HouseInSight()
    {
        foreach (Tile currentTile in brain.map.GetVisibleTiles())
        {
            if(currentTile.TileType == TileContent.House && currentTile.Position != brain.playerInfo.HouseLocation)
            {
                destinationType = currentTile.TileType;
                destination = currentTile.Position;
                return true;
            }
        }
        return false;
    }


    string SearchDirection(Point direction)
    {
        Point position = brain.playerInfo.HouseLocation;
        for (int i = 0; i < 20; i++)
        {
            Point nextPosition = position + direction;
            if (IsOutOfMap(nextPosition))
            {
                break;
            }
            else
            {
                TileContent tile = brain.map.GetTileAt(nextPosition.X, nextPosition.Y);
                if (tile == TileContent.Wall || tile == TileContent.Resource)
                {
                    destination = nextPosition;
                    return GoTo(nextPosition);
                }
            }
            position = nextPosition;
        }

        destination = position;
        return GoTo(position);
    }


    //string DropDownLow()
    //{
    //    return 
    //}
}

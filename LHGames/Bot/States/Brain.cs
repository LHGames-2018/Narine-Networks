using System;
using System.Collections.Generic;
using LHGames.Helper;
using AStar;

namespace LHGames.Helper
{
    internal class Brain
    {
        State currentState;
        State savedLastState;

        MiningState miningState = new MiningState();

        public IEnumerable<IPlayer> visiblePlayers;
        public IPlayer playerInfo;
        public Map map;

        int size;

        AStar.GridAStar astar = new AStar.GridAStar(false);
        GameInfo gameInfo;

        public Brain()
        {
            //init all states
            miningState.Init(this);

            currentState = miningState;
        }

        public string UpdateState(IPlayer playerInfo, Map map, IEnumerable<IPlayer> visiblePlayers)
        {
            this.playerInfo = playerInfo;
            this.visiblePlayers = visiblePlayers;
            this.map = map;
            size = map.VisibleDistance;

            miningState.SetMineral(FindPositionOfTile(TileContent.Resource, size));


            return currentState.Update();
        }

        public void ExitCurrentState(State state)
        {
        }

        public void SetNewState(State states)
        {
            if (currentState != null)
            {
                savedLastState = currentState;
            }

            if (CheckIfLastState())
            {
                return;
            }

            currentState = states;
            currentState.StartStates();
        }

        public string GenerateAction()
        {
            return "Halo 3 gros bb";
        }

        bool CheckIfLastState()
        {
            if (currentState == savedLastState)
            {
                return true;
            }

            return false;
        }

        public string GetDirection(Point destination)
        {
            List<Vector2> directions = FindPath(destination);
            //null check

            Point point = new Point(directions[0].x, directions[0].y);
            return AIHelper.CreateMoveAction(point);
        }

        public List<Vector2> FindPath(Point destination)
        {
            Vector2 start = new Vector2(size / 2, size / 2);
            Vector2 end = new Vector2(destination);

            char[,] charMap = MapToCharArray(size);
            char[] collisions = { 'l', 'm' };

            List<Vector2> directions = astar.FindPath(start, end, charMap, collisions);
            return directions;
        }

        char[,] MapToCharArray(int size)
        {
            char[,] charMap = new char[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    charMap[x, y] = TileToChar(map.GetTileAt(x, y));
                }
            }
            return charMap;
        }

        char TileToChar(TileContent tile)
        {
            switch (tile)
            {
                case TileContent.Empty:
                    return ' ';
                    break;
                case TileContent.Wall:
                    return ' ';
                    break;
                case TileContent.House:
                    return ' ';
                    break;
                case TileContent.Lava:
                    return 'l';
                    break;
                case TileContent.Resource:
                    return 'm';
                    break;
                case TileContent.Shop:
                    return ' ';
                    break;
                case TileContent.Player:
                    return ' ';
                    break;
            }
            return ' ';
        }


        Point FindPositionOfTile(TileContent tile, int size)
        {
            IList<Tile> tileList = (IList<Tile>)map.GetVisibleTiles();

            foreach (Tile currentTile in map.GetVisibleTiles())
            { 
                if (currentTile.TileType == tile)
                    return currentTile.Position;
            }
            return new Point(0, 0);
        }
    }
}
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
        UpgradeState upgradeState = new UpgradeState();
        CombatState combatState = new CombatState();
        StealthState stealthState = new StealthState();

        public IEnumerable<IPlayer> visiblePlayers;
        public IPlayer playerInfo;
        public Map map;

        int size;
        Dictionary<int, int> upgradeLevels;
        Dictionary<UpgradeType, int> currentUpgrade;

        AStar.GridAStar astar = new AStar.GridAStar(false);
        GameInfo gameInfo;

        public Brain()
        {
            //init all states
            miningState.Init(this);

            currentState = miningState;
            upgradeLevels = new Dictionary<int, int>() { { 1, 10000 }, { 2, 15000 }, { 3, 25000 }, { 4, 50000 }, { 5, 100000 }, { 6, int.MaxValue } };
            currentUpgrade = new Dictionary<UpgradeType, int>() { { UpgradeType.AttackPower, 0 }, { UpgradeType.CarryingCapacity, 0 }, { UpgradeType.CollectingSpeed, 0 }, { UpgradeType.Defence, 0 }, { UpgradeType.MaximumHealth, 0 } };
        }

        public string UpdateState(IPlayer playerInfo, Map map, IEnumerable<IPlayer> visiblePlayers)
        {
            this.playerInfo = playerInfo;
            this.visiblePlayers = visiblePlayers;
            this.map = map;
            size = map.VisibleDistance * 2;

            if(CanUpgrade())
            {
                currentState = upgradeState;
            }
            else
            {
                Point position = FindPositionOfTile(TileContent.Resource, size);
                miningState.SetMineral(position);
            }

            return currentState.Update();
        }

        public bool CanUpgrade()
        {
            if (playerInfo.TotalResources >= upgradeLevels[currentUpgrade[UpgradeType.CollectingSpeed] + 1])
                return true;
            return false;
        }

        public void UpgradeGear(UpgradeType type)
        {
            currentUpgrade[type]++;
        }

        public void ExitCurrentState(State state)
        {
            if (currentState != null)
            {
                savedLastState = currentState;
            }

            
        }

        void CheckBestState()
        {
            if(playerInfo.CarriedResources == playerInfo.CarryingCapacity)
            {
                
            }
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

            Console.WriteLine("Direction count " + directions.Count);
            Point point = new Point(directions[0].x, directions[0].y);
            return AIHelper.CreateMoveAction(point);
        }

        public List<Vector2> FindPath(Point destination)
        {
            Vector2 start = GlobalToLocal(playerInfo.Position);
            Vector2 end = GlobalToLocal(destination);

            char[,] charMap = MapToCharArray(size);
            char[] collisions = { 'w' };


            List<Vector2> directions = astar.FindPath(start, end, charMap, collisions);

            return directions;
        }

        Vector2 GlobalToLocal(Point point)
        {
            return new Vector2(point.X - map.XMin, point.Y - map.YMin);
        }
        Point GlobalToLocalPoint(Point point)
        {
            return new Point(point.X - map.XMin, point.Y - map.YMin);
        }

        char[,] MapToCharArray(int size)
        {
            char[,] charMap = new char[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    charMap[y, x] = TileToChar(map.GetTileAt(x + map.XMin, y + map.YMin));
                }
            }
            PrintMap(charMap, true);
            PrintMap(charMap, false);

            return charMap;
        }

        void PrintMap(char[,] map, bool debug)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write(map[x, y] + ((debug) ? "(" + x + "/" + y + ")" : ""));
                }
                Console.Write("\n");

            }
        }

        char TileToChar(TileContent tile)
        {
            switch (tile)
            {
                case TileContent.Empty:
                    return ' ';
                    break;
                case TileContent.Wall:
                    return 'w';
                    break;
                case TileContent.House:
                    return 'h';
                    break;
                case TileContent.Lava:
                    return 'l';
                    break;
                case TileContent.Resource:
                    return 'm';
                    break;
                case TileContent.Shop:
                    return 's';
                    break;
                case TileContent.Player:
                    return 'p';
                    break;
            }
            return ' ';
        }

        Point FindPositionOfTile(TileContent tile, int size)
        {
            List<Tile> location = new List<Tile>();
            foreach (Tile currentTile in map.GetVisibleTiles())
            { 
                if (currentTile.TileType == tile)
                    location.Add(currentTile);
            }

            List<List<Vector2>> ListOfPaths = new List<List<Vector2>>();
            for (int i = 0; i < location.Count; i++)
            {
                //Point point = GlobalToLocalPoint(location[i].Position);
                List <Vector2> directions = FindPath(location[i].Position);
                ListOfPaths.Add(directions);
            }

            int index = -1;
            int minDistance = 10000;
            for (int i = 0; i < ListOfPaths.Count; i++)
            {
                //Path null
                if (ListOfPaths[i] == null)
                    continue;

                if(ListOfPaths[i].Count < minDistance)
                {
                    index = i; ;
                    minDistance = ListOfPaths[i].Count;
                }
            }
        
            if (index != -1)
            {
                return location[index].Position;
            }
            return new Point(0, 0);
        }
    }
}
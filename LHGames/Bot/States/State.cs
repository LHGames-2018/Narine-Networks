using System;
using LHGames;
using LHGames.Helper;
namespace LHGames.Helper
{ 
    internal abstract class State
    {
        protected Brain brain;
        //protected GameInfo gameInfo;
        public abstract void StartStates();

        public void Init(Brain brain)
        {
            this.brain = brain;
        }

        public string Update()
        {
            return UpdateState();
        }

        protected abstract string UpdateState();

        protected virtual void ExitCurrentState()
        {

        }

        protected string GoTo(Point destination)
        {
            return brain.GetDirection(destination);
        }
        protected string ReturnHome()
        {
            //for now vue que la map sera toujours proche
            return brain.GetDirection(brain.playerInfo.HouseLocation);
        }

        protected virtual bool Adjacent(Point point)
        {
            Point diff = Difference(point, brain.playerInfo.Position);
            return (diff.X == 0 && diff.Y == 1) || (diff.X == 1 && diff.Y == 0);
        }

        Point Difference(Point pt1, Point pt2)
        {
            return new Point(Math.Abs(pt1.X - pt2.X), Math.Abs(pt1.Y - pt2.Y));
        }
    }
}
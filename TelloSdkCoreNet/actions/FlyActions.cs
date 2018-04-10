using System;

namespace TelloSdkCoreNet.actions
{
    public class FlyActions : BaseAction
    {
        private enum FlyDirections
        {
            Up,
            Down,
            Left,
            Right,
            Forward,
            Back
        }

        public FlyActions(TelloUdpClient client = null)
            : base(client)
        {
        }
        /// <summary>
        /// Flies the craft upward
        /// </summary>
        /// <param name="distance">distance in CM to fly up (valid range is 20-500)</param>
        /// <returns></returns>
        public Action FlyUpward(int distance)
        {
            return Fly(FlyDirections.Up, distance);
        }

        /// <summary>
        /// Flies the craft downward
        /// </summary>
        /// <param name="distance">distance in CM to fly down (valid range is 20-500)</param>
        /// <returns></returns>
        public Action FlyDownward(int distance)
        {
            return Fly(FlyDirections.Down, distance);
        }

        public Action FlyLeft(int distance)
        {
            return Fly(FlyDirections.Left, distance);
        }

        public Action FlyRight(int distance)
        {
            return Fly(FlyDirections.Right, distance);
        }
        public Action FlyForward(int distance)
        {
            return Fly(FlyDirections.Forward, distance);
        }
        public Action FlyBack(int distance)
        {
            return Fly(FlyDirections.Back, distance);
        }

        private Action Fly(FlyDirections direction, int distance)
        {

            if (distance < 20 || distance > 500)
            {
                throw new ArgumentException("Invalid distance value", nameof(distance));
            }
            return CreateAction(direction, distance);
        }

        private Action CreateAction(FlyDirections direction, int distance)
        {
            return new Action(direction.ToString(), $"{direction.ToString().ToLower()} {distance}", Client);
        }

    }
}

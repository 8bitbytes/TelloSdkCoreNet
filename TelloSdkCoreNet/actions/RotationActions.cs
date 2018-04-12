using System;
namespace TelloSdkCoreNet.actions
{
    public class RotationActions : BaseAction
    {
        private enum Rotations
        {
            Clockwise,
            CounterClockwise
        }

        public RotationActions(TelloUdpClient client = null)
            : base(client)
        {

        }
        public Action RotateClockwise(int degrees) => Rotate(Rotations.Clockwise, degrees);

        public Action RotateCounterClockwise(int degrees) => Rotate(Rotations.CounterClockwise, degrees);

        private Action Rotate(Rotations direction, int degrees)
        {
            if (degrees < 0 || degrees > 3600)
            {
                throw new ArgumentException("Invalid degrees value", nameof(degrees));
            }

            return CreateAction(direction, degrees);
        }

        private Action CreateAction(Rotations direction, int degrees)
        {
            return new Action(direction.ToString(), $"{getRotationString(direction)} {degrees}",Action.ActionTypes.Control, Client);
        }

        private string getRotationString(Rotations direction)
        {
            switch (direction)
            {
                case Rotations.Clockwise:
                    {
                        return "cw";
                    }
                case Rotations.CounterClockwise:
                    {
                        return "ccw";
                    }
            }
            return string.Empty;
        }

    }
}

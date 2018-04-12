namespace TelloSdkCoreNet.actions
{

    public class FlipActions : BaseAction
    {
        private enum FlipDirections
        {
            Left,
            Right,
            Forward,
            Back,
            BackLeft,
            BackRight,
            FrontLeft,
            FrontRight
        }

        public FlipActions(TelloUdpClient client = null)
            :base(client)
        {

        }
        
        public Action FlipLeft() => Flip(FlipDirections.Left);
        
        public Action FlipRight() => Flip(FlipDirections.Right);
        
        public Action FlipForward() => Flip(FlipDirections.Forward);
        
        public Action FlipBack() => Flip(FlipDirections.Back);
        
        public Action FlipBackLeft() => Flip(FlipDirections.BackLeft);
        
        public Action FlipBackRight() => Flip(FlipDirections.BackRight);
        
        public Action FlipFrontLeft() => Flip(FlipDirections.FrontLeft);
        
        public Action FlipFrontRight() => Flip(FlipDirections.FrontRight);
        
        private Action Flip(FlipDirections direction)
        {

            return CreateAction(direction);
        }

        private Action CreateAction(FlipDirections direction)
        {
            return new Action(direction.ToString(),$"flip {getFlipDirectionsString(direction)}",Action.ActionTypes.Control,Client);
        }

        private string getFlipDirectionsString(FlipDirections direction)
        {
            switch (direction)
            {
                case FlipDirections.Back:
                    {
                        return "b";
                    }
                case FlipDirections.BackLeft:
                    {
                        return "bl";
                    }
                case FlipDirections.BackRight:
                    {
                        return "br";
                    }
                case FlipDirections.Forward:
                    {
                        return "f";
                    }
                case FlipDirections.FrontLeft:
                    {
                        return "fl";
                    }
                case FlipDirections.FrontRight:
                    {
                        return "fr";
                    }
                case FlipDirections.Left:
                    {
                        return "l";
                    }
                case FlipDirections.Right:
                    {
                        return "r";
                    }
            }
            return string.Empty;
        }
        
    }
}

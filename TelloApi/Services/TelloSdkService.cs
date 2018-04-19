using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelloSdkCoreNet;
using TelloSdkCoreNet.actions;
namespace TelloApi.Services
{
    public class TelloSdkService
    {
        private SdkWrapper _wrapper;

        public TelloSdkService()
        {
            _wrapper = SdkWrapper.Instance;
        }
        public string TakeOff()
        {
            return _wrapper.BaseActions.TakeOff().Execute().ToString();
        }

        public string Land()
        {
            return _wrapper.BaseActions.Land().Execute().ToString();
        }

        public string Flip(string direction)
        {
            TelloSdkCoreNet.actions.Action action;
            switch (direction.ToLower())
            {
                case "forward":
                    {
                        action = _wrapper.FlipActions.FlipForward();
                        break;
                    }
                case "backward":
                    {
                        action = _wrapper.FlipActions.FlipBack();
                        break;
                    }
                case "left":
                    {
                        action = _wrapper.FlipActions.FlipLeft();
                        break;
                    }
                case "right":
                    {
                        action = _wrapper.FlipActions.FlipRight();
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"{direction} is not a valid flip direction");
                    }
            }
            return action.Execute().ToString();
        }
        public string Move(string direction,int distance)
        {
            TelloSdkCoreNet.actions.Action action;
            switch (direction.ToLower())
            {
                case "upward":
                    {
                        action = _wrapper.FlyActions.FlyUpward(distance);
                        break;
                    }

                case "downward":
                    {
                        action = _wrapper.FlyActions.FlyDownward(distance);
                        break;
                    }

                case "left":
                    {
                        action = _wrapper.FlyActions.FlyLeft(distance);
                        break;
                    }

                case "right":
                    {
                        action = _wrapper.FlyActions.FlyRight(distance);
                        break;
                    }
                case "backward":
                    {
                        action = _wrapper.FlyActions.FlyBack(distance);
                        break;
                    }
                case "forward":
                    {
                        action = _wrapper.FlyActions.FlyForward(distance);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"{direction} is not a valid direction");
                    }
            }

            return action.Execute().ToString();

        }

        public string Roate(string direction,int degrees)
        {
            TelloSdkCoreNet.actions.Action action;
            switch (direction.ToLower())
            {
                case "clockwise":
                    {
                        action = _wrapper.RotationActions.RotateClockwise(degrees);
                        break;
                    }
                case "counterclockwise":
                    {
                        action = _wrapper.RotationActions.RotateCounterClockwise(degrees);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException($"{direction} is not a valid rotation direction");
                    }
            }
            return action.Execute().ToString();
        }
        public string QueryBattery()
        {
            return _wrapper.BaseActions.QueryBattery().Execute().ToString();
        }
    }
}

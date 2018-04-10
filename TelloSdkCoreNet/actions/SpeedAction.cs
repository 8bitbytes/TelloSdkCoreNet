using System;
namespace TelloSdkCoreNet.actions
{
    public class SpeedAction : BaseAction
    {
        public SpeedAction(TelloUdpClient client)
            : base(client)
        {

        }

        public Action Speed(int speed)
        {
            if(speed < 1 || speed > 100)
            {
                throw new ArgumentException("Invalid speed value", nameof(speed));
            }
            return CreateAction(speed);
        }

        private Action CreateAction(int speed)
        {
            return new Action("speed", $"speed {speed}",Client);
        }
    }
}

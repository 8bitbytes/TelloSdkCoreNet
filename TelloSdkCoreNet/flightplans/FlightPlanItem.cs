using System;
namespace TelloSdkCoreNet.flightplans
{
    public class FlightPlanItem
    {
        private actions.Action _action;
        public actions.Action Action
        {
            get
            {
                return _action;
            }
            set
            {
                if(value.Type == actions.Action.ActionTypes.Read)
                {
                    throw new ArgumentException("Cannot add Read action to flight plan at this time");
                }
                _action = value;
            }
        }
        public int NumberOfTimesToExecute { get; set; }
        public double SecondsToWaitBeforeNext { get; set; }

        public FlightPlanItem()
        {
            NumberOfTimesToExecute = 1;
            SecondsToWaitBeforeNext = 2;
        }
    }
}

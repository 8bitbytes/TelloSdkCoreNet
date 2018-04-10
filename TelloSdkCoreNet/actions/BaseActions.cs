using System;
using System.Collections.Generic;

namespace TelloSdkCoreNet.actions
{
    public class BaseActions : BaseAction
    {
        private bool _inCommandMode;
        private Exception _lastException;
        private Dictionary<string, Action> _actionCache;

        public BaseActions(TelloUdpClient client)
            :base(client)
        {
            CreateActionCache();
        }
        private void CreateActionCache()
        {
            _actionCache = new Dictionary<string, Action>();
            //this saves from newing up an object each time
            var newItem = new Action("Auto takeoff", "takeoff", Client);
            _actionCache.Add("takeoff", newItem);

            newItem = new Action("Auto land", "land", Client);
            _actionCache.Add("land", newItem);

            newItem = new Action("command", "command", Client);
            _actionCache.Add("command", newItem);

            //read commands
            newItem = new Action("Get current speed", "Speed?", Client);
            _actionCache.Add("speed?", newItem);

            newItem = new Action("Get current battery percentage", "Battery?", Client);
            _actionCache.Add("battery?", newItem);

            newItem = new Action("Get current flight time", "Time?", Client);
            _actionCache.Add("time?", newItem);

        }
        public Action TakeOff() => _actionCache.GetValueOrDefault("takeoff");

        public Action Land() => _actionCache.GetValueOrDefault("land");

        public Action CommandMode() => _actionCache.GetValueOrDefault("command");


        public Action QuerySpeed() => _actionCache.GetValueOrDefault("Speed?");
        public Action QueryBattery() => _actionCache.GetValueOrDefault("Battery?");
        public Action QueryFlightTime() => _actionCache.GetValueOrDefault("Time?");

        public SdkWrapper.SdkReponses CommandModeGuard()
        {
            if (_inCommandMode)
            {
                return SdkWrapper.SdkReponses.OK;
            }

            var resp = CommandMode().Execute();
            _lastException = CommandMode().LastException;
            _inCommandMode = resp == SdkWrapper.SdkReponses.OK ? true : false;
            return resp;
        }
    }
}

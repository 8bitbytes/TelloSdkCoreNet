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
            var newItem = new Action("Auto takeoff", "takeoff",Action.ActionTypes.Control, Client);
            _actionCache.Add("takeoff", newItem);

            newItem = new Action("Auto land", "land", Action.ActionTypes.Control, Client);
            _actionCache.Add("land", newItem);

            newItem = new Action("command", "command", Action.ActionTypes.CommandMode, Client);
            _actionCache.Add("command", newItem);

            //read commands
            newItem = new Action("Get current speed", "Speed?", Action.ActionTypes.Read, Client);
            _actionCache.Add("speed?", newItem);

            newItem = new Action("Get current battery percentage", "Battery?", Action.ActionTypes.Read, Client);
            _actionCache.Add("battery?", newItem);

            newItem = new Action("Get current flight time", "Time?", Action.ActionTypes.Read, Client);
            _actionCache.Add("time?", newItem);

        }
        public Action TakeOff() => _actionCache.GetValueOrDefault("takeoff");
        public Action Land() => _actionCache.GetValueOrDefault("land");
        internal Action CommandMode() => _actionCache.GetValueOrDefault("command");
        public Action QuerySpeed() => _actionCache.GetValueOrDefault("speed?");
        public Action QueryBattery() => _actionCache.GetValueOrDefault("battery?");
        public Action QueryFlightTime() => _actionCache.GetValueOrDefault("time?");

    }
}

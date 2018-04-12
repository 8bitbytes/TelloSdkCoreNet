using System;
namespace TelloSdkCoreNet.actions
{
    public class Action: BaseAction,IAction
    {
        private string _actionName;
        private string _actionCommand;
        private ActionTypes _actionType;
        private Exception _lastException;

        public enum ActionTypes
        {
            CommandMode,
            Control,
            Read,
            Set,
        }
        public string Name => _actionName;
        public string Command => _actionCommand;
        public ActionTypes Type => _actionType;
        public Exception Error => _lastException;
               
        
        public Action(string name, string command,ActionTypes type, TelloUdpClient client = null) :
            base(client)
        {
            _actionCommand = command;
            _actionName = name;
            _actionType = type;
            
        }
        public SdkWrapper.SdkReponses Execute()
        {
            if(this.Type != ActionTypes.CommandMode)
            {
                if (!Client.CommandModeEnabled)
                {
                    var baseActions = new BaseActions(Client);
                    var resp = SendCommand(baseActions.CommandMode());
                    if(resp != SdkWrapper.SdkReponses.OK)
                    {
                        return resp;
                    }
                }
            }
            var retval = SendCommand(this);
            _lastException = base.LastException;
            return retval;
        }
    }
}

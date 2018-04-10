using System;
namespace TelloSdkCoreNet.actions
{
    public class Action: BaseAction,IAction
    {
        private string _actionName;
        private string _actionCommand;
        private Exception _lastException;
        public string ActionName => _actionName;

        public string ActionCommand => _actionCommand;

        public Exception Error => _lastException;

        
        public Action(string actionName, string actionCommand, TelloUdpClient socket = null) :
            base(socket)
        {
            _actionCommand = actionCommand;
            _actionName = actionName;
            
        }
        public SdkWrapper.SdkReponses Execute()
        {
            
            var retval = SendCommand(_actionCommand)? SdkWrapper.SdkReponses.OK 
                                              : SdkWrapper.SdkReponses.FAIL;
            _lastException = base.LastException;
            return retval;
        }
    }
}

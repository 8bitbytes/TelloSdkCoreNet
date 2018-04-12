using System;
namespace TelloSdkCoreNet.actions
{
    public abstract class BaseAction
    {
        private TelloUdpClient _client;
        private Exception _lastException;
        private string _response;

        public BaseAction(TelloUdpClient client = null)
        {
            _client = client;
        }

        public TelloUdpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public Exception LastException => _lastException;

        public string ServerResponse => _response;
                
        public SdkWrapper.SdkReponses SendCommand(Action command)
        {
            var serverResponse = SdkWrapper.SdkReponses.FAIL;
            try
            {
                if (_client == null)
                {
                    throw new Exception("Client is null");
                }
                 serverResponse = _client.SendMessage(command);
                _response = _client.ServerResponse;
            }
            catch(Exception ex)
            {
                if(command.Type == Action.ActionTypes.CommandMode)
                {
                    //drone is probably already in command mode. Continue
                    return SdkWrapper.SdkReponses.OK;
                }
                _lastException = ex;
                return SdkWrapper.SdkReponses.FAIL;
            }
            return serverResponse;
        }
    }
}

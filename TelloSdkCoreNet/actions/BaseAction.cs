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

        public TelloUdpClient Client => _client;

        public Exception LastException => _lastException;

        public string ServerResponse => _response;
                
        public bool SendCommand(string command)
        {
         
            try
            {
                if (_client == null)
                {
                    throw new Exception("Client is null");
                }
                _client.SendMessage(command);
                _response = _client.ServerResponse;
            }
            catch(Exception ex)
            {
                _lastException = ex;
                return false;
            }
            return true;
        }
    }
}

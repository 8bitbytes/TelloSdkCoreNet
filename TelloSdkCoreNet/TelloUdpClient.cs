using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TelloSdkCoreNet
{
    public class TelloUdpClient
    {

        private UdpClient _client;
        private IPAddress _ipaddress;
        private IPEndPoint _endpoint;
        private string _serverReponse;
        private bool _commandMode = false;

        public TelloUdpClient(IPAddress ipaddress,IPEndPoint endpoint)
        {
            _client = new UdpClient();
            _ipaddress = ipaddress;
            _endpoint = endpoint;
        }

        public string ServerResponse => _serverReponse;
        public bool CommandModeEnabled => _commandMode;
        public SdkWrapper.SdkReponses SendMessage(actions.Action action)
        {
            if(action.Type == actions.Action.ActionTypes.CommandMode && _commandMode)
            {
                return SdkWrapper.SdkReponses.OK;
            }
            if (_client == null)
            {
                return SdkWrapper.SdkReponses.FAIL;
            }
            _client.Connect(_endpoint);
            var data = Encoding.ASCII.GetBytes(action.Command);
            _client.Send(data, data.Length);
            var RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, _endpoint.Port + 1);
            _client.Client.ReceiveTimeout = 2500;
            var receiveBytes = _client.Receive(ref RemoteIpEndPoint);
            _serverReponse = Encoding.ASCII.GetString(receiveBytes);

            if(action.Type == actions.Action.ActionTypes.Read)
            {
                return _serverReponse == "FAIL" ? SdkWrapper.SdkReponses.FAIL
                                                : SdkWrapper.SdkReponses.OK;
            }
            if(action.Type == actions.Action.ActionTypes.CommandMode && _serverReponse == "OK")
            {
                _commandMode = true;
            }
            return _serverReponse == "OK" ? SdkWrapper.SdkReponses.OK
                                          : SdkWrapper.SdkReponses.FAIL;
        }

        public void Close()
        {
            _client.Close();
        }
    }
}

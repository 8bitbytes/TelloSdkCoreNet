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

        public TelloUdpClient(IPAddress ipaddress,IPEndPoint endpoint)
        {
            _client = new UdpClient();
            _ipaddress = ipaddress;
            _endpoint = endpoint;
        }

        public string ServerResponse => _serverReponse;
        public bool SendMessage(string message)
        {
            if (_client == null)
            {
                return false;
            }
            _client.Connect(_endpoint);
            var data = Encoding.ASCII.GetBytes(message);
            _client.Send(data, data.Length);
            var RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var receiveBytes = _client.Receive(ref RemoteIpEndPoint);
            _serverReponse = Encoding.ASCII.GetString(receiveBytes);
            return true;
        }

        public void Close()
        {
            _client.Close();
        }
    }
}

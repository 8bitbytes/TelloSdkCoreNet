using System;
using System.Net;
using TelloSdkCoreNet.actions;

namespace TelloSdkCoreNet
{
    public class SdkWrapper : IDisposable
    {
        private TelloUdpClient _udpClient;
        private IPAddress _ipAddress;
        private IPEndPoint _endpoint;
        private Exception _lastException;
        public enum SdkReponses
        {
            OK,
            FAIL
        }
        
        public FlipActions FlipActions;
        public FlyActions FlyActions;
        public RotationActions RotationActions;
        public BaseActions BaseActions;
        public SpeedAction SpeedAction;

        public Exception LastException => _lastException;
       public SdkWrapper()
        {
            CreateClient();
            FlipActions = new FlipActions(_udpClient);
            FlyActions = new FlyActions(_udpClient);
            RotationActions = new RotationActions(_udpClient);
            BaseActions = new BaseActions(_udpClient);
            SpeedAction = new SpeedAction(_udpClient);
        }

        public void ShutDown(){
            _udpClient.Close();
        }
        public void Dispose()
        {
            _udpClient.Close();
        }

        private void CreateClient()
        {
            //_ipAddress = IPAddress.Parse("10.30.54.81");
            _ipAddress = IPAddress.Parse("192.168.10.1");
            _endpoint = new IPEndPoint(_ipAddress, 8889);
            _udpClient = new TelloUdpClient(_ipAddress,_endpoint);
        }
        public SdkReponses ExecuteAction(actions.Action action)
        {
            if (BaseActions.CommandModeGuard() == SdkReponses.FAIL)
            {
                _lastException = BaseActions.LastException;
                return SdkReponses.FAIL;
            }
            return action.Execute();
        }
        public SdkReponses ExecuteActions(actions.Action[] actions,int secondsBetweenCommand)
        {
            if (BaseActions.CommandModeGuard() == SdkReponses.FAIL)
            {
                _lastException = BaseActions.CommandMode().LastException;
                return SdkReponses.FAIL;
            }

            foreach(var action in actions)
            {
               var resp = action.Execute();
                if(resp == SdkReponses.FAIL)
                {
                    _lastException = action.LastException;
                    return resp;
                }
                System.Threading.Thread.Sleep(secondsBetweenCommand * 1000);
            }
            return SdkReponses.OK;
        }
    }
}

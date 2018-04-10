using System;
using System.Linq;
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

        public SdkReponses ExecuteFlightPlan(flightplans.FlightPlan flightPlan)
        {
            var nonCommandActions = flightPlan.Items.Where(a => a.Action.Type == TelloSdkCoreNet.actions.Action.ActionTypes.Read).ToArray();
            if (nonCommandActions.Length > 0)
            {
                throw new ArgumentException("Flight plans cannot include query actions");
            }
            foreach (var fpi in flightPlan.Items)
            {
                var exeCount = fpi.NumberOfTimesToExecute;
                while (exeCount > 0)
                {
                    fpi.Action.Client = _udpClient;
                    if (fpi.Action.Execute() == SdkReponses.FAIL)
                    {
                        return SdkReponses.FAIL;
                    }
                    System.Threading.Thread.Sleep((int)fpi.SecondsToWaitBeforeNext * 1000);
                    exeCount--;
                }
            }
            return SdkReponses.OK;
        }

        private void CreateClient()
        {
            _ipAddress = IPAddress.Parse("192.168.10.1");
            _endpoint = new IPEndPoint(_ipAddress, 8889);
            _udpClient = new TelloUdpClient(_ipAddress,_endpoint);
        }
        
        
    }
}

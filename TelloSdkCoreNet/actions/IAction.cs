using System;
using System.Collections.Generic;
using System.Text;

namespace TelloSdkCoreNet.actions
{
    public interface IAction
    {
        SdkWrapper.SdkReponses Execute();
    }
}

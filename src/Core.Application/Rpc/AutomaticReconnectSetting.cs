using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Rpc
{
    public class AutomaticReconnectSetting : IRetryPolicy
    {
        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            if (retryContext.PreviousRetryCount < 3)
            {
                return TimeSpan.FromSeconds(1);
            }
            else
            {
                return TimeSpan.FromSeconds(8);
            }
        }
    }
}

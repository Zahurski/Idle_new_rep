using System;

namespace IdleTycoon.Ads.Exceptions
{
    public class NotInitializeSDK : Exception
    {
        public NotInitializeSDK()
        {
        }

        public NotInitializeSDK(string message) : base(message)
        {
        }
    }
}
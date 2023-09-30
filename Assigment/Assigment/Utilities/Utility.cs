using Serilog;
using System;
using System.Data;
using System.Runtime.ExceptionServices;

namespace Assigment.Utilities
{
    public class Utility
    {
        internal static void RetainExceptionStackTrace(Exception ex)
        {
            var capture = ExceptionDispatchInfo.Capture(ex);
            if (capture != null)
            {
                capture.Throw();
            }
        }
    }
}

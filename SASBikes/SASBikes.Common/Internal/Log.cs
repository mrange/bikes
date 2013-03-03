// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

using System;
using SASBikes.Common.AppServices;
using SASBikes.Common.DataModel;
using SASBikes.Common.WindowsAdaptors;

namespace SASBikes.Common.Source.Common
{
    static partial class Log
    {
        readonly static IConcurrentQueue<string> s_errors = new ConcurrentQueue<string> ();
        static partial void Partial_LogMessage(Level level, string message)
        {
            switch (level)
            {
                case Level.Success:
                case Level.HighLight:
                case Level.Info:
                case Level.Warning:
                    break;
                case Level.Error:
                case Level.Exception:
                default:
                    s_errors.Enqueue (message ?? "");
                    Services.App.Async_Invoke(AsyncGroup.Log_UpdateErrors, Log_UpdateErrors);
                    break;
            }
        }

        static void Log_UpdateErrors()
        {
            var state = Services.App.State;
            if (state == null)
            {
                return;
            }

            string error;
            while (s_errors.TryDequeue(out error))
            {
                state.State_Errors.Add(
                    new Error(state.Context) 
                    {
                        Error_TimeStamp = DateTime.Now  ,
                        Error_Message   = error         ,
                    });
            }
        }
    }
}

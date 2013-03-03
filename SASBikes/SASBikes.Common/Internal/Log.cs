// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

using SASBikes.Common.AppServices;

namespace SASBikes.Common.Source.Common
{
    static partial class Log
    {
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
                    Services.Log.Error(message ?? "");
                    break;
            }
        }

    }
}

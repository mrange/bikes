using SASBikes.DataModel;
using SASBikes.Source.Extensions;

namespace SASBikes.Common
{
    partial class SuspensionManager
    {
        const string ApplicationState = "ApplicationState";

        public static State AppState { get; set; }

        static partial void Loading_SessionState()
        {
            var applicationState = SessionState.Lookup(ApplicationState) as string;
            if (!applicationState.IsNullOrEmpty())
            {
                AppState = applicationState.UnserializeFromString();
            }
        }

        static partial void Saving_SessionState()
        {
            SessionState[ApplicationState] = AppState.SerializeToString();
        }

    }
}
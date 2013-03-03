// ----------------------------------------------------------------------------------------------
// Copyright (c) Mårten Rånge.
// ----------------------------------------------------------------------------------------------
// This source code is subject to terms and conditions of the Microsoft Public License. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// If you cannot locate the  Microsoft Public License, please send an email to 
// dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
//  by the terms of the Microsoft Public License.
// ----------------------------------------------------------------------------------------------
// You must not remove this notice, or any other, from this software.
// ----------------------------------------------------------------------------------------------

using SASBikes.Common.DataModel;
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
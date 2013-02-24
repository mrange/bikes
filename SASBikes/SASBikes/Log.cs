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

using System;
using SASBikes.DataModel;

namespace SASBikes.Source.Common
{
    static partial class Log
    {
        static partial void Partial_LogMessage(Log.Level level, string message)
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
                    App.Value.AppState.State_Errors.Add(
                        new Error(App.Value.AppState.Context) 
                        {
                            Error_Message = message,
                        });
                    break;
            }
        }
    }
}
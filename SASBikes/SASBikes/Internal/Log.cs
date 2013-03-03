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

// ReSharper disable InconsistentNaming

using SASBikes.Common.AppServices;

namespace SASBikes.Source.Common
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
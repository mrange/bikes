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

using System;
using SASBikes.Common.DataModel;
using SASBikes.Common.WindowsAdaptors;

namespace SASBikes.Common.AppServices
{
    public sealed class LogService : IService
    {
        readonly IConcurrentQueue<string> s_errors = new ConcurrentQueue<string> ();
        bool m_isRunning;

        public void Start()
        {
            m_isRunning = true;
            Services.App.Async_Invoke(AsyncGroup.Log_UpdateErrors, Log_UpdateErrors);
        }

        public void Stop()
        {
            m_isRunning = false;
        }

        public void Error (string message)
        {
            s_errors.Enqueue(message ?? "");
            if (m_isRunning)
            {
                Services.App.Async_Invoke(AsyncGroup.Log_UpdateErrors, Log_UpdateErrors);
            }
        }

        void Log_UpdateErrors()
        {
            if (!m_isRunning)
            {
                return;
            }

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
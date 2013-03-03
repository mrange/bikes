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

namespace SASBikes.Common.WindowsAdaptors
{
#if SILVERLIGHT || WINDOWS_PHONE
    using System;
    using System.Windows.Threading;
    using Windows.UI.Core;

    public class Runner : IRunner
    {
        public readonly Dispatcher Dispatcher;
    
        public Runner(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
    
        public void RunOnApplicationIdle(Action action)
        {
            if (action == null)
            {
                return;
            }
    
            if (Dispatcher == null)
            {
                return;
            }

            Dispatcher.BeginInvoke(action);
        }
    }
#else
    using System;
    using Windows.UI.Core;

    public class Runner : IRunner
    {
        public readonly CoreDispatcher CoreDispatcher;

        public Runner(CoreDispatcher coreDispatcher)
        {
            CoreDispatcher = coreDispatcher ?? CoreWindow.GetForCurrentThread().Dispatcher;
        }

        public void RunOnApplicationIdle(Action action)
        {
            if (action == null)
            {
                return;
            }

            var task = CoreDispatcher.RunIdleAsync(e => action());
        }
    }
#endif

    public partial interface IRunner
    {
        void RunOnApplicationIdle (Action action);
    }

}
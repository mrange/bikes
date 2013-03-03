// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # template file (.tt)                                                      #
// ############################################################################





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
using System.Threading;
using SASBikes.Common.Source.Common;

namespace SASBikes.Common.AppServices
{
    partial interface IService
    {
        void Start ();
        void Stop ();
    }

    public static partial class Services
    {
        public static readonly AppService App = new AppService()      ;
        public static readonly LocatorService Locator = new LocatorService()      ;
        public static readonly StationsService Stations = new StationsService()      ;

        public static void Start()
        {
            var state = SetState(States.Started);
            if (state == States.Stopped)
            {
                StartService (Stations);
                StartService (Locator);
                StartService (App);
            }
        }

        public static void Stop()
        {
            var state = SetState(States.Stopped);
            if (state == States.Started)
            {
                StopService (App);
                StopService (Locator);
                StopService (Stations);
            }
        }

        static void StopService(this IService service)
        {
            if (service != null)
            {
                try
                {
                    service.Stop();
                }
                catch (Exception exc)
                {
                    Log.Exception ("Failed to stop service {0}: {1}", service.GetType().Name, exc);
                }
            }
            
        }

        static void StartService(this IService service)
        {
            if (service != null)
            {
                try
                {
                    service.Start();
                }
                catch (Exception exc)
                {
                    Log.Exception ("Failed to start service {0}: {1}", service.GetType().Name, exc);
                }
            }

        }

        enum States
        {
            Stopped = 1,
            Started = 2,
        }

        static int s_state = (int)States.Stopped; 

        static States SetState(States states)
        {
            var state = Interlocked.Exchange(ref s_state, (int) states);
            return (States) state;
        }

    }
}



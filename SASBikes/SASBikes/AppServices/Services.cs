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
using SASBikes.Source.Common;
using Windows.UI.Core;

namespace SASBikes.AppServices
{
    interface IService
    {
        void Start ();
        void Stop ();
    }

    static class Services
    {
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

        public static readonly SchedulerService         Scheduler       = new SchedulerService()        ;
        public static readonly UpdateLocationService    UpdateLocation  = new UpdateLocationService()   ;
        public static readonly UpdateStationsService    UpdateStations  = new UpdateStationsService()   ;

        enum States
        {
            Stopped = 1,
            Started = 2,
        }

        static int s_state = (int)States.Stopped; 

        public static void Start()
        {
            var state = SetState(States.Started);
            if (state == States.Stopped)
            {
                UpdateLocation.StartService();
                UpdateStations.StartService();
                Scheduler.StartService();
            }
        }

        static States SetState(States states)
        {
            var state = Interlocked.Exchange(ref s_state, (int) states);
            return (States) state;
        }

        public static void Stop()
        {
            var state = SetState(States.Stopped);
            if (state == States.Started)
            {
                Scheduler.StopService();
                UpdateStations.StopService();
                UpdateLocation.StopService();
            }
        }

    }
}

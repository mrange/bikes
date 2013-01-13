using System;
using System.Threading;
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
                    // TODO: Log
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
                    // TODO: Log
                }
            }

        }

        public static readonly LogService               Log             = new LogService()              ;
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
                Log.StartService();
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
                Log.StopService();
            }
        }

    }
}

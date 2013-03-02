



// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # text template file (.tt)                                                      #
// ############################################################################



// ############################################################################
// @@@ SKIPPING (Blacklisted): C:\temp\GitHub\bikes\SASBikes\Global_AssemblyInfo.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppStateService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\Generated_Services.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LocatorService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\StationsService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModel.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelBase.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelCollection.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelContext.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelSerializer.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\Generated_DataModel.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IDataModelEntity.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IUnserializeErrorReporter.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Extensions.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\GeoCoordinate.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Include_T4Include.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Internal\Log.cs
// @@@ SKIPPING (Blacklisted): C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Properties\AssemblyInfo.cs
// ############################################################################
// Certains directives such as #define and // Resharper comments has to be 
// moved to top in order to work properly    
// ############################################################################
// ReSharper disable CheckNamespace
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable InconsistentNaming
// ReSharper disable InvocationIsSkipped
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantAssignment
// ReSharper disable RedundantNameQualifier
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppStateService.cs
namespace SASBikes.Common
{
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SASBikes.Common.DataModel;
    using SASBikes.Common.Source.Common;
    using SASBikes.Common.Source.Extensions;
    using Windows.UI.Core;
    
    namespace SASBikes.Common.AppServices
    {
        public enum AsyncGroup
        {
            Model_UpdateStations            ,
            Model_UpdateMyPosition          ,
            Map_UpdateView                  ,
            Map_UpdateMapStations           ,
            Map_UpdateMyPosition            ,
            LocatorService_UpdateMyPosition ,
            StationsService_UpdateStations  ,
            Log_UpdateErrors
        }
    
        sealed class AppService : IService
        {
            public State State;
            public CoreDispatcher Dispatcher;
    
            readonly ConcurrentDictionary<AsyncGroup, bool> m_dispatchedAsyncCalls = new ConcurrentDictionary<AsyncGroup, bool>();
    
            public void Start()
            {
                m_dispatchedAsyncCalls.Clear();
            }
    
            public void Stop()
            {
                m_dispatchedAsyncCalls.Clear();
            }
    
            public void Async_Invoke(AsyncGroup group, Action action)
            {
                if (action == null)
                {
                    return;
                }
    
                var dispatcher = Dispatcher;
    
                if (dispatcher == null)
                {
                    return;
                }
    
                if (!m_dispatchedAsyncCalls.TryAdd(group, true))
                {
                    return;
                }
    
                var task = dispatcher.RunIdleAsync(
                    e =>
                        {
                            try
                            {
                                action();
                            }
                            catch (Exception exc)
                            {
                                Log.Exception ("Failed to dispatch async invoke {0}: {1}", group, exc);
                            }
                            finally
                            {
                                bool val;
                                m_dispatchedAsyncCalls.TryRemove(group, out val);
                            }
                        });
            }
    
            public void UpdateStations (string xmlData)
            {
                var state = State;
                if (state == null)
                {
                    return;
                }
    
                var stations = CreateStations(state.Context, xmlData).ToArray();
    
                state.State_Stations.Clear ();
                foreach (var station in stations)
                {
                    state.State_Stations.Add(station);
                }
            }
    
            static IEnumerable<Station> CreateStations (
                DataModelContext context, 
                string xmlData
                )
            {
                var doc = XDocument.Parse(xmlData).Document;
                var markers = doc
                    .Elements("carto")
                    .Elements("markers")
                    .Elements("marker")
                    .ToArray();
    
                for (var index = 0; index < markers.Length; index++)
                {
                    var marker = markers[index];
                    var station = new Station(context)
                                      {
                                          Station_Name = marker.GetAttributeValue("name", "NoName"),
                                          Station_Number = marker.GetAttributeValue("number", "0").Parse(0),
                                          Station_Address = marker.GetAttributeValue("address", "NoAddress"),
                                          Station_FullAddress = marker.GetAttributeValue("fullAddress", "NoAddress"),
                                          Station_La = marker.GetAttributeValue("lat", "57.69").Parse(57.69),
                                          Station_Lo = marker.GetAttributeValue("lng", "11.95").Parse(11.95),
                                          Station_IsOpen = marker.GetAttributeValue("open", "0") == "1",
                                          Station_IsBonus = marker.GetAttributeValue("bonus", "0") == "1",
                                      };
                    yield return station;
                }
            }
    
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppStateService.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\Generated_Services.cs
namespace SASBikes.Common
{
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
    
        static partial class Services
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
    
    
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\Generated_Services.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LocatorService.cs
namespace SASBikes.Common
{
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
    using SASBikes.Common.Source.Common;
    using Windows.Devices.Geolocation;
    
    namespace SASBikes.Common.AppServices
    {
        sealed class LocatorService : IService
        {
            Geolocator m_locator;
            Geoposition m_lastKnownPosition;
    
            public void Start()
            {
                m_locator = new Geolocator();
                m_locator.PositionChanged += PositionChanged_Locator;
            }
    
            void PositionChanged_Locator(Geolocator sender, PositionChangedEventArgs args)
            {
                try
                {
                    m_lastKnownPosition = args.Position;
                    Services.App.Async_Invoke(
                        AsyncGroup.LocatorService_UpdateMyPosition,
                        LocatorService_UpdateMyPosition
                        );
                }
                catch (Exception exc)
                {
                    Log.Exception ("Failed to update my position", exc);
                }
            }
    
            void LocatorService_UpdateMyPosition()
            {
                var state = Services.App.State;
                var lastKnownPosition = m_lastKnownPosition;
                if (lastKnownPosition != null && state != null)
                {
                    state.State_MyLa = m_lastKnownPosition.Coordinate.Latitude;
                    state.State_MyLo = m_lastKnownPosition.Coordinate.Longitude;
                }
            }
    
            public void Stop()
            {
                if (m_locator != null)
                {
                    m_locator.PositionChanged -= PositionChanged_Locator;
                    m_locator = null;
                }
    
            }
    
            
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LocatorService.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\StationsService.cs
namespace SASBikes.Common
{
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
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using SASBikes.Common.Source.Common;
    using SASBikes.Common.Source.Extensions;
    
    namespace SASBikes.Common.AppServices
    {
        sealed class StationsService : IService
        {
            const int Delay_InitialUpdateStations   =        5   *1000   ;
            const int Delay_UpdateStations          = 5     *60  *1000   ;
    
            CancellationTokenSource m_source;
            Task m_updateTask;
            string m_lastXmlData;
    
            public void Start()
            {
                m_source = new CancellationTokenSource();
                var token = m_source.Token;
    
                m_updateTask = Task
                        .Delay(Delay_InitialUpdateStations, token)
                        .ContinueWith(t => UpdateStations(token), token)
                        ;
            }
    
            public void Stop()
            {
                if (m_source != null)
                {
                    m_source.Cancel();
                    m_source.Dispose();
                    m_source = null;
                }
    
                m_updateTask = null;
            }
    
            void UpdateStations(CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                
                        var xmlData = httpClient.GetStringAsync(@"http://www.goteborgbikes.se/index.php/service/carto")
                            .Result
                            ?? ""
                            ;
    
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }
                
                        if (!xmlData.IsNullOrWhiteSpace())
                        {
                            m_lastXmlData = xmlData;
                            Services.App.Async_Invoke (AsyncGroup.StationsService_UpdateStations, StationsService_UpdateStations);
                        }
                    }
                }
                catch (Exception exc)
                {
                    Log.Exception ("Failed to get station data: {0}", exc);
                }
    
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                
                m_updateTask =
                    Task
                        .Delay(Delay_UpdateStations, cancellationToken)
                        .ContinueWith(t => UpdateStations(cancellationToken), cancellationToken)
                        ;
            }
    
            void StationsService_UpdateStations()
            {
                if (!m_lastXmlData.IsNullOrWhiteSpace())
                {
                    Services.App.UpdateStations(m_lastXmlData);
                }
            }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\StationsService.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModel.cs
namespace SASBikes.Common
{
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
    using SASBikes.Common.AppServices;
    
    namespace SASBikes.Common.DataModel
    {
        public partial class State
        {
            partial void Changed__State_MyLa(double oldValue, double newValue)
            {
                Services.App.Async_Invoke(AsyncGroup.Model_UpdateMyPosition, Model_UpdateMyPosition);
            }
    
            partial void Changed__State_MyLo(double oldValue, double newValue)
            {
                Services.App.Async_Invoke(AsyncGroup.Model_UpdateMyPosition, Model_UpdateMyPosition);
            }
    
            void Model_UpdateMyPosition()
            {
                Model_UpdateStations ();
    
                if (State_IsTrackingMyPosition)
                {
                    State_La = State_MyLa;
                    State_Lo = State_MyLo;
                }
            }
    
            void Model_UpdateStations ()
            {
                var location = new GeoCoordinate(State_MyLa, State_MyLo);
    
                for (int index = 0; index < State_Stations.Count; index++)
                {
                    var station = State_Stations[index];
    
                    station.Station_Distance = location.DistanceTo(new GeoCoordinate(
                                                                       station.Station_La,
                                                                       station.Station_Lo
                                                                       ));
                }
            }
    
            partial void Changed__State_IsTrackingMyPosition(bool oldValue, bool newValue)
            {
                if (newValue)
                {
                    State_La        = State_MyLa;
                    State_Lo        = State_MyLo;
                    State_ZoomLevel = 20        ;
                }
            }
    
            partial void Changed__State_Stations(StationList oldValue, StationList newValue)
            {
                Services.App.Async_Invoke(AsyncGroup.Model_UpdateStations, Model_UpdateStations);
            }
    
            partial void CollectionChanged__State_Stations(StationList value, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                Services.App.Async_Invoke(AsyncGroup.Model_UpdateStations, Model_UpdateStations);
            }
        }
    
        partial class Error
        {
            partial void Changed__Error_TimeStamp(DateTime oldValue, DateTime newValue)
            {
                Error_FormattedTimeStamp = newValue.ToString ("HH:mm:ss");
            }    
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModel.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelBase.cs
namespace SASBikes.Common
{
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
    
    
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    
    namespace SASBikes.Common.DataModel
    {
        public abstract partial class DataModelBase : INotifyPropertyChanged, IDataModelEntity
        {
            readonly DataModelContext m_context;
    
            protected DataModelBase(DataModelContext context)
            {
                m_context = context;
            }
    
            public DataModelContext Context 
            {
                get
                {
                    return m_context;
                }
            }
    
            public event PropertyChangedEventHandler PropertyChanged;
    
            protected virtual void Raise_PropertyChanged([CallerMemberName] string propertyName = null)
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelBase.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelCollection.cs
namespace SASBikes.Common
{
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
    
    using System.Collections.ObjectModel;
    
    namespace SASBikes.Common.DataModel
    {
        public partial class DataModelCollection<T> : ObservableCollection<T>, IDataModelEntity
        {
            readonly DataModelContext m_context;
    
            public DataModelCollection(DataModelContext context)
            {
                m_context = context;
            }
    
            public DataModelContext Context
            {
                get
                {
                    return m_context;
                }
            }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelCollection.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelContext.cs
namespace SASBikes.Common
{
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
    
    namespace SASBikes.Common.DataModel
    {
        public sealed partial class DataModelContext
        {
            public bool IsSuppressingEvents;        
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelContext.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelSerializer.cs
namespace SASBikes.Common
{
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
    
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using SASBikes.Common.Source.Extensions;
    
    namespace SASBikes.Common.DataModel
    {
        public partial class DataModelSerializer
        {
            public static readonly CultureInfo SerializeCulture = CultureInfo.InvariantCulture;
    
            public static readonly XName    NodeName           = "N";
            public static readonly XName    NameAttributeName  = "n";
            public static readonly string   RootName           = "Root";
    
            public static XElement CreateElement(
                string name,
                params object[] elements
                )
            {
                return new XElement(NodeName , new XAttribute(NameAttributeName, name ?? "") , elements);
            }
    
            static XElement CreateTextElement(string name, string value)
            {
                return CreateElement(name, new XText(value ?? ""));
            }
    
            public static XElement Serialize(this string value, string name)
            {
                return CreateTextElement(name, value);
            }
    
            public static void Unserialize (
                    this XElement element
                ,   DataModelContext context
                ,   IUnserializeErrorReporter reporter
                ,   ref DataModelCollection<string> instance
                )
            {
                instance = new DataModelCollection<string> (context);
    
                if (element == null)
                {
                    return;
                }
    
                foreach (var subElement in element.Elements (NodeName))
                {
                    instance.Add (subElement.Value);                                
                }
            }
    
            public static void Unserialize(
                this XElement element
                , DataModelContext context
                , IUnserializeErrorReporter reporter
                , ref string instance
                )
            {
                if (element == null)
                {
                    return;
                }
    
                instance = element.Value;
            }
    
            public static XElement Serialize(this double value, string name)
            {
                return CreateTextElement(name, value.ToString(SerializeCulture));
            }
    
            public static void Unserialize(
                this XElement element
                , DataModelContext context
                , IUnserializeErrorReporter reporter
                , ref double instance
                )
            {
                if (element == null)
                {
                    return;
                }
    
                instance = element.Value.Parse(SerializeCulture, 0.0);
            }
    
            public static XElement Serialize(this int value, string name)
            {
                return CreateTextElement(name, value.ToString(SerializeCulture));
            }
    
            public static void Unserialize(
                this XElement element
                , DataModelContext context
                , IUnserializeErrorReporter reporter
                , ref int instance
                )
            {
                if (element == null)
                {
                    return;
                }
    
                instance = element.Value.Parse(SerializeCulture, 0);
            }
    
            public static XElement Serialize(this bool value, string name)
            {
                return CreateTextElement(name, value.ToString());
            }
    
            public static void Unserialize(
                this XElement element
                , DataModelContext context
                , IUnserializeErrorReporter reporter
                , ref bool instance
                )
            {
                if (element == null)
                {
                    return;
                }
    
                instance = element.Value.Parse(SerializeCulture, false);
            }
    
    
            public static string SerializeToString(this State state)
            {
                var doc = new XDocument(state.Serialize(RootName));
    
                using (var sw = new StringWriter())
                {
                    doc.Save(sw);
                    return sw.ToString();
                }
            }
    
            public static State UnserializeFromString(this string value)
            {
                var doc = XDocument.Parse(value ?? "");
    
                var context = new DataModelContext
                                  {
                                      IsSuppressingEvents = true
                                  };
    
                State state = null;
                doc
                    .Document
                    .Elements(NodeName)
                    .FirstOrDefault()
                    .Unserialize(context, null, ref state)
                    ;
    
                context.IsSuppressingEvents = false;
    
                return state;
            }
    
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelSerializer.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\Generated_DataModel.cs
namespace SASBikes.Common
{
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
    
    
    // ############################################################################
    // #                                                                          #
    // #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
    // #                                                                          #
    // # This means that any edits to the .cs file will be lost when its          #
    // # regenerated. Changes should instead be applied to the corresponding      #
    // # template file (.tt)                                                      #
    // ############################################################################
    
    
    
    
    
    
    namespace SASBikes.Common.DataModel
    {
    
        using System.Collections.ObjectModel;
        using System.Collections.Specialized;
        using System.Linq;
        using System.Xml.Linq;
    
    
        public sealed partial class StateList : DataModelCollection<State>
        {
            public StateList (DataModelContext context) : base (context)
            {
            }
        }
    
        public sealed partial class State : DataModelBase 
        {
            public State (DataModelContext context) : base (context)
            {
                _State_IsTrackingMyPosition = true   ;
                _State_ZoomLevel = default (double)   ;
                _State_Lo = default (double)   ;
                _State_La = default (double)   ;
                _State_MyLo = default (double)   ;
                _State_MyLa = default (double)   ;
                _State_StationName = ""   ;
                _State_SearchingFor = ""   ;
                _State_Stations = new StationList (context)   ;
                if (_State_Stations != null)
                {
                    _State_Stations.CollectionChanged += CollectionChanged__State_Stations;
                }
                _State_Errors = new ErrorList (context)   ;
                if (_State_Errors != null)
                {
                    _State_Errors.CollectionChanged += CollectionChanged__State_Errors;
                }
            }
    
            // --------------------------------------------------------------------
            public bool State_IsTrackingMyPosition
            {
                get
                {
                    return _State_IsTrackingMyPosition;
                }
                set
                {
                    if (_State_IsTrackingMyPosition != value)
                    {
                        var oldValue = _State_IsTrackingMyPosition; 
    
                        _State_IsTrackingMyPosition = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_IsTrackingMyPosition (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            bool _State_IsTrackingMyPosition;
            // --------------------------------------------------------------------
            partial void Changed__State_IsTrackingMyPosition (bool oldValue, bool newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double State_ZoomLevel
            {
                get
                {
                    return _State_ZoomLevel;
                }
                set
                {
                    if (_State_ZoomLevel != value)
                    {
                        var oldValue = _State_ZoomLevel; 
    
                        _State_ZoomLevel = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_ZoomLevel (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _State_ZoomLevel;
            // --------------------------------------------------------------------
            partial void Changed__State_ZoomLevel (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double State_Lo
            {
                get
                {
                    return _State_Lo;
                }
                set
                {
                    if (_State_Lo != value)
                    {
                        var oldValue = _State_Lo; 
    
                        _State_Lo = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_Lo (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _State_Lo;
            // --------------------------------------------------------------------
            partial void Changed__State_Lo (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double State_La
            {
                get
                {
                    return _State_La;
                }
                set
                {
                    if (_State_La != value)
                    {
                        var oldValue = _State_La; 
    
                        _State_La = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_La (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _State_La;
            // --------------------------------------------------------------------
            partial void Changed__State_La (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double State_MyLo
            {
                get
                {
                    return _State_MyLo;
                }
                set
                {
                    if (_State_MyLo != value)
                    {
                        var oldValue = _State_MyLo; 
    
                        _State_MyLo = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_MyLo (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _State_MyLo;
            // --------------------------------------------------------------------
            partial void Changed__State_MyLo (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double State_MyLa
            {
                get
                {
                    return _State_MyLa;
                }
                set
                {
                    if (_State_MyLa != value)
                    {
                        var oldValue = _State_MyLa; 
    
                        _State_MyLa = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_MyLa (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _State_MyLa;
            // --------------------------------------------------------------------
            partial void Changed__State_MyLa (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public string State_StationName
            {
                get
                {
                    return _State_StationName;
                }
                set
                {
                    if (_State_StationName != value)
                    {
                        var oldValue = _State_StationName; 
    
                        _State_StationName = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_StationName (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _State_StationName;
            // --------------------------------------------------------------------
            partial void Changed__State_StationName (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public string State_SearchingFor
            {
                get
                {
                    return _State_SearchingFor;
                }
                set
                {
                    if (_State_SearchingFor != value)
                    {
                        var oldValue = _State_SearchingFor; 
    
                        _State_SearchingFor = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_SearchingFor (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _State_SearchingFor;
            // --------------------------------------------------------------------
            partial void Changed__State_SearchingFor (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public StationList State_Stations
            {
                get
                {
                    return _State_Stations;
                }
                set
                {
                    if (_State_Stations != value)
                    {
                        var oldValue = _State_Stations; 
    
                        if (oldValue != null)
                        {
                            oldValue.CollectionChanged -= CollectionChanged__State_Stations;
                        }
                        _State_Stations = value;
                        if (value != null)
                        {
                            value.CollectionChanged += CollectionChanged__State_Stations;
                        }
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_Stations (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            StationList _State_Stations;
            void CollectionChanged__State_Stations (object sender, NotifyCollectionChangedEventArgs e)
            {
                if (!Context.IsSuppressingEvents)
                {
                    CollectionChanged__State_Stations (_State_Stations, e);
                }
            }
            // --------------------------------------------------------------------
            partial void CollectionChanged__State_Stations (StationList value, NotifyCollectionChangedEventArgs e);
            partial void Changed__State_Stations (StationList oldValue, StationList newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public ErrorList State_Errors
            {
                get
                {
                    return _State_Errors;
                }
                set
                {
                    if (_State_Errors != value)
                    {
                        var oldValue = _State_Errors; 
    
                        if (oldValue != null)
                        {
                            oldValue.CollectionChanged -= CollectionChanged__State_Errors;
                        }
                        _State_Errors = value;
                        if (value != null)
                        {
                            value.CollectionChanged += CollectionChanged__State_Errors;
                        }
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__State_Errors (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            ErrorList _State_Errors;
            void CollectionChanged__State_Errors (object sender, NotifyCollectionChangedEventArgs e)
            {
                if (!Context.IsSuppressingEvents)
                {
                    CollectionChanged__State_Errors (_State_Errors, e);
                }
            }
            // --------------------------------------------------------------------
            partial void CollectionChanged__State_Errors (ErrorList value, NotifyCollectionChangedEventArgs e);
            partial void Changed__State_Errors (ErrorList oldValue, ErrorList newValue);
            // --------------------------------------------------------------------
    
    
        }
        public sealed partial class StationList : DataModelCollection<Station>
        {
            public StationList (DataModelContext context) : base (context)
            {
            }
        }
    
        public sealed partial class Station : DataModelBase 
        {
            public Station (DataModelContext context) : base (context)
            {
                _Station_Name = ""   ;
                _Station_Number = default (int)   ;
                _Station_Address = ""   ;
                _Station_FullAddress = ""   ;
                _Station_Lo = default (double)   ;
                _Station_La = default (double)   ;
                _Station_IsOpen = default (bool)   ;
                _Station_IsBonus = default (bool)   ;
                _Station_Distance = default (double)   ;
            }
    
            // --------------------------------------------------------------------
            public string Station_Name
            {
                get
                {
                    return _Station_Name;
                }
                set
                {
                    if (_Station_Name != value)
                    {
                        var oldValue = _Station_Name; 
    
                        _Station_Name = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_Name (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _Station_Name;
            // --------------------------------------------------------------------
            partial void Changed__Station_Name (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public int Station_Number
            {
                get
                {
                    return _Station_Number;
                }
                set
                {
                    if (_Station_Number != value)
                    {
                        var oldValue = _Station_Number; 
    
                        _Station_Number = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_Number (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            int _Station_Number;
            // --------------------------------------------------------------------
            partial void Changed__Station_Number (int oldValue, int newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public string Station_Address
            {
                get
                {
                    return _Station_Address;
                }
                set
                {
                    if (_Station_Address != value)
                    {
                        var oldValue = _Station_Address; 
    
                        _Station_Address = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_Address (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _Station_Address;
            // --------------------------------------------------------------------
            partial void Changed__Station_Address (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public string Station_FullAddress
            {
                get
                {
                    return _Station_FullAddress;
                }
                set
                {
                    if (_Station_FullAddress != value)
                    {
                        var oldValue = _Station_FullAddress; 
    
                        _Station_FullAddress = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_FullAddress (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _Station_FullAddress;
            // --------------------------------------------------------------------
            partial void Changed__Station_FullAddress (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double Station_Lo
            {
                get
                {
                    return _Station_Lo;
                }
                set
                {
                    if (_Station_Lo != value)
                    {
                        var oldValue = _Station_Lo; 
    
                        _Station_Lo = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_Lo (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _Station_Lo;
            // --------------------------------------------------------------------
            partial void Changed__Station_Lo (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double Station_La
            {
                get
                {
                    return _Station_La;
                }
                set
                {
                    if (_Station_La != value)
                    {
                        var oldValue = _Station_La; 
    
                        _Station_La = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_La (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _Station_La;
            // --------------------------------------------------------------------
            partial void Changed__Station_La (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public bool Station_IsOpen
            {
                get
                {
                    return _Station_IsOpen;
                }
                set
                {
                    if (_Station_IsOpen != value)
                    {
                        var oldValue = _Station_IsOpen; 
    
                        _Station_IsOpen = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_IsOpen (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            bool _Station_IsOpen;
            // --------------------------------------------------------------------
            partial void Changed__Station_IsOpen (bool oldValue, bool newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public bool Station_IsBonus
            {
                get
                {
                    return _Station_IsBonus;
                }
                set
                {
                    if (_Station_IsBonus != value)
                    {
                        var oldValue = _Station_IsBonus; 
    
                        _Station_IsBonus = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_IsBonus (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            bool _Station_IsBonus;
            // --------------------------------------------------------------------
            partial void Changed__Station_IsBonus (bool oldValue, bool newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public double Station_Distance
            {
                get
                {
                    return _Station_Distance;
                }
                set
                {
                    if (_Station_Distance != value)
                    {
                        var oldValue = _Station_Distance; 
    
                        _Station_Distance = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Station_Distance (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            double _Station_Distance;
            // --------------------------------------------------------------------
            partial void Changed__Station_Distance (double oldValue, double newValue);
            // --------------------------------------------------------------------
    
    
        }
        public sealed partial class ErrorList : DataModelCollection<Error>
        {
            public ErrorList (DataModelContext context) : base (context)
            {
            }
        }
    
        public sealed partial class Error : DataModelBase 
        {
            public Error (DataModelContext context) : base (context)
            {
                _Error_TimeStamp = default (DateTime)   ;
                _Error_FormattedTimeStamp = ""   ;
                _Error_Message = ""   ;
            }
    
            // --------------------------------------------------------------------
            public DateTime Error_TimeStamp
            {
                get
                {
                    return _Error_TimeStamp;
                }
                set
                {
                    if (_Error_TimeStamp != value)
                    {
                        var oldValue = _Error_TimeStamp; 
    
                        _Error_TimeStamp = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Error_TimeStamp (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            DateTime _Error_TimeStamp;
            // --------------------------------------------------------------------
            partial void Changed__Error_TimeStamp (DateTime oldValue, DateTime newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public string Error_FormattedTimeStamp
            {
                get
                {
                    return _Error_FormattedTimeStamp;
                }
                private set
                {
                    if (_Error_FormattedTimeStamp != value)
                    {
                        var oldValue = _Error_FormattedTimeStamp; 
    
                        _Error_FormattedTimeStamp = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Error_FormattedTimeStamp (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _Error_FormattedTimeStamp;
            // --------------------------------------------------------------------
            partial void Changed__Error_FormattedTimeStamp (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
            // --------------------------------------------------------------------
            public string Error_Message
            {
                get
                {
                    return _Error_Message;
                }
                set
                {
                    if (_Error_Message != value)
                    {
                        var oldValue = _Error_Message; 
    
                        _Error_Message = value;
    
                        if (!Context.IsSuppressingEvents)
                        {
                            Changed__Error_Message (oldValue, value);
    
                            Raise_PropertyChanged ();
                        }
                    }
                }
            }
            // --------------------------------------------------------------------
            string _Error_Message;
            // --------------------------------------------------------------------
            partial void Changed__Error_Message (string oldValue, string newValue);
            // --------------------------------------------------------------------
    
    
        }
    
        static partial class DataModelSerializer
        {
            public static XElement Serialize (this StateList instance, string name)
            {
                if (instance == null)
                {
                    return null;
                }
    
                return CreateElement (
                        name
                    ,   instance.Select ((v,i) => v.Serialize (i.ToString()))
                    );
    
            }
    
            public static XElement Serialize (this State instance, string name)
            {
                if (instance == null)
                {
                    return null;
                }
                return CreateElement (
                        name
                    ,   instance.State_IsTrackingMyPosition.Serialize ("IsTrackingMyPosition")
                    ,   instance.State_ZoomLevel.Serialize ("ZoomLevel")
                    ,   instance.State_Lo.Serialize ("Lo")
                    ,   instance.State_La.Serialize ("La")
                    ,   instance.State_MyLo.Serialize ("MyLo")
                    ,   instance.State_MyLa.Serialize ("MyLa")
                    ,   instance.State_StationName.Serialize ("StationName")
                    ,   instance.State_SearchingFor.Serialize ("SearchingFor")
                    ,   instance.State_Stations.Serialize ("Stations")
                    );
            }
    
            public static void Unserialize (
                    this XElement element
                ,   DataModelContext context
                ,   IUnserializeErrorReporter reporter
                ,   ref StateList instance
                )
            {
                instance = new StateList (context);
    
                if (element == null)
                {
                    return;
                }
    
                foreach (var subElement in element.Elements (NodeName))
                {
                    State subInstance = null;
                    
                    subElement.Unserialize (
                        context,
                        reporter,
                        ref subInstance
                        );
    
                    instance.Add (subInstance);                                
                }
            }
    
            public static void Unserialize (
                    this XElement element
                ,   DataModelContext context
                ,   IUnserializeErrorReporter reporter
                ,   ref State instance
                )
            {
                instance = new State (context);
    
                if (element == null)
                {
                    return;
                }
    
                foreach (var subElement in element.Elements(NodeName))
                {
                    var nameAttribute = subElement.Attribute(NameAttributeName);
                    if (nameAttribute == null)
                    {
                        continue;
                    }
    
                    var name = nameAttribute.Value;
    
                    switch (name)
                    {
                        case "IsTrackingMyPosition":
                            {
                                var value = true;
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_IsTrackingMyPosition = value;                                
                            }
                            break;
                        case "ZoomLevel":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_ZoomLevel = value;                                
                            }
                            break;
                        case "Lo":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_Lo = value;                                
                            }
                            break;
                        case "La":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_La = value;                                
                            }
                            break;
                        case "MyLo":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_MyLo = value;                                
                            }
                            break;
                        case "MyLa":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_MyLa = value;                                
                            }
                            break;
                        case "StationName":
                            {
                                var value = "";
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_StationName = value;                                
                            }
                            break;
                        case "SearchingFor":
                            {
                                var value = "";
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_SearchingFor = value;                                
                            }
                            break;
                        case "Stations":
                            {
                                var value = new StationList (context);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.State_Stations = value;                                
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
    
    
            public static XElement Serialize (this StationList instance, string name)
            {
                if (instance == null)
                {
                    return null;
                }
    
                return CreateElement (
                        name
                    ,   instance.Select ((v,i) => v.Serialize (i.ToString()))
                    );
    
            }
    
            public static XElement Serialize (this Station instance, string name)
            {
                if (instance == null)
                {
                    return null;
                }
                return CreateElement (
                        name
                    ,   instance.Station_Name.Serialize ("Name")
                    ,   instance.Station_Number.Serialize ("Number")
                    ,   instance.Station_Address.Serialize ("Address")
                    ,   instance.Station_FullAddress.Serialize ("FullAddress")
                    ,   instance.Station_Lo.Serialize ("Lo")
                    ,   instance.Station_La.Serialize ("La")
                    ,   instance.Station_IsOpen.Serialize ("IsOpen")
                    ,   instance.Station_IsBonus.Serialize ("IsBonus")
                    ,   instance.Station_Distance.Serialize ("Distance")
                    );
            }
    
            public static void Unserialize (
                    this XElement element
                ,   DataModelContext context
                ,   IUnserializeErrorReporter reporter
                ,   ref StationList instance
                )
            {
                instance = new StationList (context);
    
                if (element == null)
                {
                    return;
                }
    
                foreach (var subElement in element.Elements (NodeName))
                {
                    Station subInstance = null;
                    
                    subElement.Unserialize (
                        context,
                        reporter,
                        ref subInstance
                        );
    
                    instance.Add (subInstance);                                
                }
            }
    
            public static void Unserialize (
                    this XElement element
                ,   DataModelContext context
                ,   IUnserializeErrorReporter reporter
                ,   ref Station instance
                )
            {
                instance = new Station (context);
    
                if (element == null)
                {
                    return;
                }
    
                foreach (var subElement in element.Elements(NodeName))
                {
                    var nameAttribute = subElement.Attribute(NameAttributeName);
                    if (nameAttribute == null)
                    {
                        continue;
                    }
    
                    var name = nameAttribute.Value;
    
                    switch (name)
                    {
                        case "Name":
                            {
                                var value = "";
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_Name = value;                                
                            }
                            break;
                        case "Number":
                            {
                                var value = default (int);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_Number = value;                                
                            }
                            break;
                        case "Address":
                            {
                                var value = "";
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_Address = value;                                
                            }
                            break;
                        case "FullAddress":
                            {
                                var value = "";
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_FullAddress = value;                                
                            }
                            break;
                        case "Lo":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_Lo = value;                                
                            }
                            break;
                        case "La":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_La = value;                                
                            }
                            break;
                        case "IsOpen":
                            {
                                var value = default (bool);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_IsOpen = value;                                
                            }
                            break;
                        case "IsBonus":
                            {
                                var value = default (bool);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_IsBonus = value;                                
                            }
                            break;
                        case "Distance":
                            {
                                var value = default (double);
    
                                subElement.Unserialize (
                                    context,
                                    reporter,
                                    ref value
                                    );       
                                
                                instance.Station_Distance = value;                                
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
    
    
    
    
        }
    
    }
    
    
    
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\Generated_DataModel.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IDataModelEntity.cs
namespace SASBikes.Common
{
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
    
    namespace SASBikes.Common.DataModel
    {
        public partial interface IDataModelEntity
        {
            DataModelContext Context { get; }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IDataModelEntity.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IUnserializeErrorReporter.cs
namespace SASBikes.Common
{
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
    
    namespace SASBikes.Common.DataModel
    {
        public interface IUnserializeErrorReporter
        {
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IUnserializeErrorReporter.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Extensions.cs
namespace SASBikes.Common
{
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
    using System.Xml.Linq;
    
    namespace SASBikes.Common
    {
        public static class Extensions
        {
            const double EarthMeanRadius = 6371009;
    
            public static string GetAttributeValue(
                this XElement element,
                XName name,
                string defaultValue
                )
            {
                if (element == null)
                {
                    return defaultValue;
                }
    
                if (name == null)
                {
                    return defaultValue;
                }
    
                var attribute = element.Attribute(name);
    
                if (attribute == null)
                {
                    return defaultValue;
                }
    
                return attribute.Value ?? defaultValue;
            }
    
            public static double Asin(this double d)
            {
                return Math.Asin(d);
            }
    
            public static double Sqrt(this double d)
            {
                return Math.Sqrt(d);
            }
    
            public static double Cos(this double d)
            {
                return Math.Cos(d);
            }
    
            public static double Haversine(this double d)
            {
                var x = Math.Sin(d/2);
                return x*x;
            }
    
            public static double DistanceTo(this GeoCoordinate location, GeoCoordinate otherLocation)
            {
                var slo = location.NormalizedLo;
                var flo = otherLocation.NormalizedLo;
                var dlo = slo - flo;
                var dla = location.NormalizedLa - otherLocation.NormalizedLa;
    
                // http://en.wikipedia.org/wiki/Great_circle_distance
    
                return EarthMeanRadius*2*(dlo.Haversine() + slo.Cos()*flo.Cos()*dla.Haversine());
            }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Extensions.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\GeoCoordinate.cs
namespace SASBikes.Common
{
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
    
    namespace SASBikes.Common
    {
        public struct GeoCoordinate
        {
            public double La;
            public double Lo;
    
            public GeoCoordinate(double la, double lo)
            {
                La = la;
                Lo = lo;
            }
    
            public double NormalizedLa
            {
                get
                {
                    var laf = La / 180.0;
                    var t = Math.Truncate(laf);
                  
                    return (laf - t) * 180.0;
                    
                }
            }
        
            public double NormalizedLo
            {
                get
                {
                    var lof = Lo / 180.0;
                    var t = Math.Truncate(lof);
                  
                    return (lof - t) * 180.0;
                    
                }
            }
    
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\GeoCoordinate.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Include_T4Include.cs
namespace SASBikes.Common
{
    
    // ############################################################################
    // #                                                                          #
    // #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
    // #                                                                          #
    // # This means that any edits to the .cs file will be lost when its          #
    // # regenerated. Changes should instead be applied to the corresponding      #
    // # text template file (.tt)                                                      #
    // ############################################################################
    
    
    
    // ############################################################################
    // @@@ INCLUDING: https://raw.github.com/mrange/T4Include/master/Common/Log.cs
    // @@@ INCLUDE_FOUND: Generated_Log.cs
    // @@@ INCLUDING: https://raw.github.com/mrange/T4Include/master/Extensions/BasicExtensions.cs
    // @@@ INCLUDE_FOUND: ../Common/Array.cs
    // @@@ INCLUDE_FOUND: ../Common/Config.cs
    // @@@ INCLUDE_FOUND: ../Common/Log.cs
    // @@@ INCLUDING: https://raw.github.com/mrange/T4Include/master/Extensions/ParseExtensions.cs
    // @@@ INCLUDE_FOUND: ../Common/Config.cs
    // @@@ INCLUDING: https://raw.github.com/mrange/T4Include/master/Common/Generated_Log.cs
    // @@@ INCLUDING: https://raw.github.com/mrange/T4Include/master/Common/Array.cs
    // @@@ INCLUDING: https://raw.github.com/mrange/T4Include/master/Common/Config.cs
    // @@@ SKIPPING (Already seen): https://raw.github.com/mrange/T4Include/master/Common/Log.cs
    // @@@ SKIPPING (Already seen): https://raw.github.com/mrange/T4Include/master/Common/Config.cs
    // ############################################################################
    // Certains directives such as #define and // Resharper comments has to be 
    // moved to top in order to work properly    
    // ############################################################################
    // ############################################################################
    
    // ############################################################################
    // @@@ BEGIN_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Log.cs
    namespace SASBikes.Common
    {
        // ----------------------------------------------------------------------------------------------
        // Copyright (c) M�rten R�nge.
        // ----------------------------------------------------------------------------------------------
        // This source code is subject to terms and conditions of the Microsoft Public License. A 
        // copy of the license can be found in the License.html file at the root of this distribution. 
        // If you cannot locate the  Microsoft Public License, please send an email to 
        // dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
        //  by the terms of the Microsoft Public License.
        // ----------------------------------------------------------------------------------------------
        // You must not remove this notice, or any other, from this software.
        // ----------------------------------------------------------------------------------------------
        
        
        
        namespace Source.Common
        {
            using System;
            using System.Globalization;
        
            static partial class Log
            {
                static partial void Partial_LogLevel (Level level);
                static partial void Partial_LogMessage (Level level, string message);
                static partial void Partial_ExceptionOnLog (Level level, string format, object[] args, Exception exc);
        
                public static void LogMessage (Level level, string format, params object[] args)
                {
                    try
                    {
                        Partial_LogLevel (level);
                        Partial_LogMessage (level, GetMessage (format, args));
                    }
                    catch (Exception exc)
                    {
                        Partial_ExceptionOnLog (level, format, args, exc);
                    }
                    
                }
        
                static string GetMessage (string format, object[] args)
                {
                    format = format ?? "";
                    try
                    {
                        return (args == null || args.Length == 0)
                                   ? format
                                   : string.Format (Config.DefaultCulture, format, args)
                            ;
                    }
                    catch (FormatException)
                    {
        
                        return format;
                    }
                }
            }
        }
    }
    // @@@ END_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Log.cs
    // ############################################################################
    
    // ############################################################################
    // @@@ BEGIN_INCLUDE: https://raw.github.com/mrange/T4Include/master/Extensions/BasicExtensions.cs
    namespace SASBikes.Common
    {
        // ----------------------------------------------------------------------------------------------
        // Copyright (c) M�rten R�nge.
        // ----------------------------------------------------------------------------------------------
        // This source code is subject to terms and conditions of the Microsoft Public License. A 
        // copy of the license can be found in the License.html file at the root of this distribution. 
        // If you cannot locate the  Microsoft Public License, please send an email to 
        // dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
        //  by the terms of the Microsoft Public License.
        // ----------------------------------------------------------------------------------------------
        // You must not remove this notice, or any other, from this software.
        // ----------------------------------------------------------------------------------------------
        
        
        
        namespace Source.Extensions
        {
            using System;
            using System.Collections.Generic;
            using System.Globalization;
            using System.IO;
            using System.Reflection;
        
            using Source.Common;
        
            static partial class BasicExtensions
            {
                public static bool IsNullOrWhiteSpace (this string v)
                {
                    return string.IsNullOrWhiteSpace (v);
                }
        
                public static bool IsNullOrEmpty (this string v)
                {
                    return string.IsNullOrEmpty (v);
                }
        
                public static T FirstOrReturn<T>(this T[] values, T defaultValue)
                {
                    if (values == null)
                    {
                        return defaultValue;
                    }
        
                    if (values.Length == 0)
                    {
                        return defaultValue;
                    }
        
                    return values[0];
                }
        
                public static T FirstOrReturn<T>(this IEnumerable<T> values, T defaultValue)
                {
                    if (values == null)
                    {
                        return defaultValue;
                    }
        
                    foreach (var value in values)
                    {
                        return value;
                    }
        
                    return defaultValue;
                }
        
                public static string DefaultTo (this string v, string defaultValue = null)
                {
                    return !v.IsNullOrEmpty () ? v : (defaultValue ?? "");
                }
        
                public static IEnumerable<T> DefaultTo<T>(
                    this IEnumerable<T> values, 
                    IEnumerable<T> defaultValue = null
                    )
                {
                    return values ?? defaultValue ?? Array<T>.Empty;
                }
        
                public static T[] DefaultTo<T>(this T[] values, T[] defaultValue = null)
                {
                    return values ?? defaultValue ?? Array<T>.Empty;
                }
        
                public static T DefaultTo<T>(this T v, T defaultValue = default (T))
                    where T : struct, IEquatable<T>
                {
                    return !v.Equals (default (T)) ? v : defaultValue;
                }
        
                public static string FormatWith (this string format, CultureInfo cultureInfo, params object[] args)
                {
                    return string.Format (cultureInfo, format ?? "", args.DefaultTo ());
                }
        
                public static string FormatWith (this string format, params object[] args)
                {
                    return format.FormatWith (Config.DefaultCulture, args);
                }
        
                public static TValue Lookup<TKey, TValue>(
                    this IDictionary<TKey, TValue> dictionary, 
                    TKey key, 
                    TValue defaultValue = default (TValue))
                {
                    if (dictionary == null)
                    {
                        return defaultValue;
                    }
        
                    TValue value;
                    return dictionary.TryGetValue (key, out value) ? value : defaultValue;
                }
        
                public static TValue GetOrAdd<TKey, TValue>(
                    this IDictionary<TKey, TValue> dictionary, 
                    TKey key, 
                    TValue defaultValue = default (TValue))
                {
                    if (dictionary == null)
                    {
                        return defaultValue;
                    }
        
                    TValue value;
                    if (!dictionary.TryGetValue (key, out value))
                    {
                        value = defaultValue;
                        dictionary[key] = value;
                    }
        
                    return value;
                }
        
                public static TValue GetOrAdd<TKey, TValue>(
                    this IDictionary<TKey, TValue> dictionary,
                    TKey key,
                    Func<TValue> valueCreator
                    )
                {
                    if (dictionary == null)
                    {
                        return valueCreator ();
                    }
        
                    TValue value;
                    if (!dictionary.TryGetValue (key, out value))
                    {
                        value = valueCreator ();
                        dictionary[key] = value;
                    }
        
                    return value;
                }
        
                public static void DisposeNoThrow (this IDisposable disposable)
                {
                    try
                    {
                        if (disposable != null)
                        {
                            disposable.Dispose ();
                        }
                    }
                    catch (Exception exc)
                    {
                        Log.Exception ("DisposeNoThrow: Dispose threw: {0}", exc);
                    }
                }
        
                public static TTo CastTo<TTo> (this object value, TTo defaultValue)
                {
                    return value is TTo ? (TTo) value : defaultValue;
                }
        
                public static string Concatenate (this IEnumerable<string> values, string delimiter = null, int capacity = 16)
                {
                    values = values ?? Array<string>.Empty;
                    delimiter = delimiter ?? ", ";
        
                    return string.Join (delimiter, values);
                }
        
                public static string GetResourceString (this Assembly assembly, string name, string defaultValue = null)
                {
                    defaultValue = defaultValue ?? "";
        
                    if (assembly == null)
                    {
                        return defaultValue;
                    }
        
                    var stream = assembly.GetManifestResourceStream (name ?? "");
                    if (stream == null)
                    {
                        return defaultValue;
                    }
        
                    using (stream)
                    using (var streamReader = new StreamReader (stream))
                    {
                        return streamReader.ReadToEnd ();
                    }
                }
        
                public static IEnumerable<string> ReadLines (this TextReader textReader)
                {
                    if (textReader == null)
                    {
                        yield break;
                    }
        
                    string line;
        
                    while ((line = textReader.ReadLine ()) != null)
                    {
                        yield return line;
                    }
                }
        
        #if !NETFX_CORE
                public static IEnumerable<Type> GetInheritanceChain (this Type type)
                {
                    while (type != null)
                    {
                        yield return type;
                        type = type.BaseType;
                    }
                }
        #endif
            }
        }
    }
    // @@@ END_INCLUDE: https://raw.github.com/mrange/T4Include/master/Extensions/BasicExtensions.cs
    // ############################################################################
    
    // ############################################################################
    // @@@ BEGIN_INCLUDE: https://raw.github.com/mrange/T4Include/master/Extensions/ParseExtensions.cs
    namespace SASBikes.Common
    {
        
        
        
        // ############################################################################
        // #                                                                          #
        // #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
        // #                                                                          #
        // # This means that any edits to the .cs file will be lost when its          #
        // # regenerated. Changes should instead be applied to the corresponding      #
        // # template file (.tt)                                                      #
        // ############################################################################
        
        
        
        
        
        
        
        namespace Source.Extensions
        {
            using System;
            using System.Collections.Generic;
            using System.Globalization;
        
            using Source.Common;
        
            static partial class ParseExtensions
            {
                static readonly Dictionary<Type, Func<object>> s_defaultValues = new Dictionary<Type, Func<object>> 
                    {
        #if !T4INCLUDE__SUPPRESS_BOOLEAN_PARSE_EXTENSIONS
                        { typeof(Boolean)      , () => default (Boolean)},
                        { typeof(Boolean?)     , () => default (Boolean?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_CHAR_PARSE_EXTENSIONS
                        { typeof(Char)      , () => default (Char)},
                        { typeof(Char?)     , () => default (Char?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_SBYTE_PARSE_EXTENSIONS
                        { typeof(SByte)      , () => default (SByte)},
                        { typeof(SByte?)     , () => default (SByte?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_INT16_PARSE_EXTENSIONS
                        { typeof(Int16)      , () => default (Int16)},
                        { typeof(Int16?)     , () => default (Int16?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_INT32_PARSE_EXTENSIONS
                        { typeof(Int32)      , () => default (Int32)},
                        { typeof(Int32?)     , () => default (Int32?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_INT64_PARSE_EXTENSIONS
                        { typeof(Int64)      , () => default (Int64)},
                        { typeof(Int64?)     , () => default (Int64?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_BYTE_PARSE_EXTENSIONS
                        { typeof(Byte)      , () => default (Byte)},
                        { typeof(Byte?)     , () => default (Byte?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_UINT16_PARSE_EXTENSIONS
                        { typeof(UInt16)      , () => default (UInt16)},
                        { typeof(UInt16?)     , () => default (UInt16?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_UINT32_PARSE_EXTENSIONS
                        { typeof(UInt32)      , () => default (UInt32)},
                        { typeof(UInt32?)     , () => default (UInt32?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_UINT64_PARSE_EXTENSIONS
                        { typeof(UInt64)      , () => default (UInt64)},
                        { typeof(UInt64?)     , () => default (UInt64?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_SINGLE_PARSE_EXTENSIONS
                        { typeof(Single)      , () => default (Single)},
                        { typeof(Single?)     , () => default (Single?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_DOUBLE_PARSE_EXTENSIONS
                        { typeof(Double)      , () => default (Double)},
                        { typeof(Double?)     , () => default (Double?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_DECIMAL_PARSE_EXTENSIONS
                        { typeof(Decimal)      , () => default (Decimal)},
                        { typeof(Decimal?)     , () => default (Decimal?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_TIMESPAN_PARSE_EXTENSIONS
                        { typeof(TimeSpan)      , () => default (TimeSpan)},
                        { typeof(TimeSpan?)     , () => default (TimeSpan?)},
        #endif
        #if !T4INCLUDE__SUPPRESS_DATETIME_PARSE_EXTENSIONS
                        { typeof(DateTime)      , () => default (DateTime)},
                        { typeof(DateTime?)     , () => default (DateTime?)},
        #endif
                    };
                static readonly Dictionary<Type, Func<string, CultureInfo, object>> s_parsers = new Dictionary<Type, Func<string, CultureInfo, object>> 
                    {
        #if !T4INCLUDE__SUPPRESS_BOOLEAN_PARSE_EXTENSIONS
                        { typeof(Boolean)  , (s, ci) => { Boolean value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Boolean?) , (s, ci) => { Boolean value; return s.TryParse(ci, out value) ? (object)(Boolean?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_CHAR_PARSE_EXTENSIONS
                        { typeof(Char)  , (s, ci) => { Char value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Char?) , (s, ci) => { Char value; return s.TryParse(ci, out value) ? (object)(Char?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_SBYTE_PARSE_EXTENSIONS
                        { typeof(SByte)  , (s, ci) => { SByte value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(SByte?) , (s, ci) => { SByte value; return s.TryParse(ci, out value) ? (object)(SByte?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_INT16_PARSE_EXTENSIONS
                        { typeof(Int16)  , (s, ci) => { Int16 value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Int16?) , (s, ci) => { Int16 value; return s.TryParse(ci, out value) ? (object)(Int16?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_INT32_PARSE_EXTENSIONS
                        { typeof(Int32)  , (s, ci) => { Int32 value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Int32?) , (s, ci) => { Int32 value; return s.TryParse(ci, out value) ? (object)(Int32?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_INT64_PARSE_EXTENSIONS
                        { typeof(Int64)  , (s, ci) => { Int64 value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Int64?) , (s, ci) => { Int64 value; return s.TryParse(ci, out value) ? (object)(Int64?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_BYTE_PARSE_EXTENSIONS
                        { typeof(Byte)  , (s, ci) => { Byte value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Byte?) , (s, ci) => { Byte value; return s.TryParse(ci, out value) ? (object)(Byte?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_UINT16_PARSE_EXTENSIONS
                        { typeof(UInt16)  , (s, ci) => { UInt16 value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(UInt16?) , (s, ci) => { UInt16 value; return s.TryParse(ci, out value) ? (object)(UInt16?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_UINT32_PARSE_EXTENSIONS
                        { typeof(UInt32)  , (s, ci) => { UInt32 value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(UInt32?) , (s, ci) => { UInt32 value; return s.TryParse(ci, out value) ? (object)(UInt32?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_UINT64_PARSE_EXTENSIONS
                        { typeof(UInt64)  , (s, ci) => { UInt64 value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(UInt64?) , (s, ci) => { UInt64 value; return s.TryParse(ci, out value) ? (object)(UInt64?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_SINGLE_PARSE_EXTENSIONS
                        { typeof(Single)  , (s, ci) => { Single value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Single?) , (s, ci) => { Single value; return s.TryParse(ci, out value) ? (object)(Single?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_DOUBLE_PARSE_EXTENSIONS
                        { typeof(Double)  , (s, ci) => { Double value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Double?) , (s, ci) => { Double value; return s.TryParse(ci, out value) ? (object)(Double?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_DECIMAL_PARSE_EXTENSIONS
                        { typeof(Decimal)  , (s, ci) => { Decimal value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(Decimal?) , (s, ci) => { Decimal value; return s.TryParse(ci, out value) ? (object)(Decimal?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_TIMESPAN_PARSE_EXTENSIONS
                        { typeof(TimeSpan)  , (s, ci) => { TimeSpan value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(TimeSpan?) , (s, ci) => { TimeSpan value; return s.TryParse(ci, out value) ? (object)(TimeSpan?)value : null;}},
        #endif
        #if !T4INCLUDE__SUPPRESS_DATETIME_PARSE_EXTENSIONS
                        { typeof(DateTime)  , (s, ci) => { DateTime value; return s.TryParse(ci, out value) ? (object)value : null;}},
                        { typeof(DateTime?) , (s, ci) => { DateTime value; return s.TryParse(ci, out value) ? (object)(DateTime?)value : null;}},
        #endif
                    };
        
                public static bool CanParse (this Type type)
                {
                    if (type == null)
                    {
                        return false;
                    }
        
                    return s_parsers.ContainsKey (type);
                }
        
                public static object GetParsedDefaultValue (this Type type)
                {
                    type = type ?? typeof (object);
        
                    Func<object> getValue;
        
                    return s_defaultValues.TryGetValue (type, out getValue) ? getValue () : null;
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, Type type, out object value)
                {
                    value = null;
                    if (type == null)
                    {
                        return false;
                    }                
                    
                    Func<string, CultureInfo, object> parser;
        
                    if (s_parsers.TryGetValue (type, out parser))
                    {
                        value = parser (s, cultureInfo);
                    }
        
                    return value != null;
                }
        
                public static bool TryParse (this string s, Type type, out object value)
                {
                    return s.TryParse (Config.DefaultCulture, type, out value);
                }
        
                public static object Parse (this string s, CultureInfo cultureInfo, Type type, object defaultValue)
                {
                    object value;
                    return s.TryParse (cultureInfo, type, out value) ? value : defaultValue;
                }
        
                public static object Parse (this string s, Type type, object defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, type, defaultValue);
                }
        
                // Boolean (BoolLike)
        
        #if !T4INCLUDE__SUPPRESS_BOOLEAN_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Boolean value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Boolean Parse (this string s, CultureInfo cultureInfo, Boolean defaultValue)
                {
                    Boolean value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Boolean Parse (this string s, Boolean defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Boolean value)
                {
                    return Boolean.TryParse (s ?? "", out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_BOOLEAN_PARSE_EXTENSIONS
        
                // Char (CharLike)
        
        #if !T4INCLUDE__SUPPRESS_CHAR_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Char value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Char Parse (this string s, CultureInfo cultureInfo, Char defaultValue)
                {
                    Char value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Char Parse (this string s, Char defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Char value)
                {
                    return Char.TryParse (s ?? "", out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_CHAR_PARSE_EXTENSIONS
        
                // SByte (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_SBYTE_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out SByte value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static SByte Parse (this string s, CultureInfo cultureInfo, SByte defaultValue)
                {
                    SByte value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static SByte Parse (this string s, SByte defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out SByte value)
                {
                    return SByte.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_SBYTE_PARSE_EXTENSIONS
        
                // Int16 (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_INT16_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Int16 value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Int16 Parse (this string s, CultureInfo cultureInfo, Int16 defaultValue)
                {
                    Int16 value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Int16 Parse (this string s, Int16 defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Int16 value)
                {
                    return Int16.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_INT16_PARSE_EXTENSIONS
        
                // Int32 (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_INT32_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Int32 value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Int32 Parse (this string s, CultureInfo cultureInfo, Int32 defaultValue)
                {
                    Int32 value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Int32 Parse (this string s, Int32 defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Int32 value)
                {
                    return Int32.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_INT32_PARSE_EXTENSIONS
        
                // Int64 (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_INT64_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Int64 value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Int64 Parse (this string s, CultureInfo cultureInfo, Int64 defaultValue)
                {
                    Int64 value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Int64 Parse (this string s, Int64 defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Int64 value)
                {
                    return Int64.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_INT64_PARSE_EXTENSIONS
        
                // Byte (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_BYTE_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Byte value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Byte Parse (this string s, CultureInfo cultureInfo, Byte defaultValue)
                {
                    Byte value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Byte Parse (this string s, Byte defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Byte value)
                {
                    return Byte.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_BYTE_PARSE_EXTENSIONS
        
                // UInt16 (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_UINT16_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out UInt16 value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static UInt16 Parse (this string s, CultureInfo cultureInfo, UInt16 defaultValue)
                {
                    UInt16 value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static UInt16 Parse (this string s, UInt16 defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out UInt16 value)
                {
                    return UInt16.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_UINT16_PARSE_EXTENSIONS
        
                // UInt32 (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_UINT32_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out UInt32 value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static UInt32 Parse (this string s, CultureInfo cultureInfo, UInt32 defaultValue)
                {
                    UInt32 value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static UInt32 Parse (this string s, UInt32 defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out UInt32 value)
                {
                    return UInt32.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_UINT32_PARSE_EXTENSIONS
        
                // UInt64 (IntLike)
        
        #if !T4INCLUDE__SUPPRESS_UINT64_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out UInt64 value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static UInt64 Parse (this string s, CultureInfo cultureInfo, UInt64 defaultValue)
                {
                    UInt64 value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static UInt64 Parse (this string s, UInt64 defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out UInt64 value)
                {
                    return UInt64.TryParse (s ?? "", NumberStyles.Integer, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_UINT64_PARSE_EXTENSIONS
        
                // Single (FloatLike)
        
        #if !T4INCLUDE__SUPPRESS_SINGLE_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Single value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Single Parse (this string s, CultureInfo cultureInfo, Single defaultValue)
                {
                    Single value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Single Parse (this string s, Single defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Single value)
                {                                                  
                    return Single.TryParse (s ?? "", NumberStyles.Float, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_SINGLE_PARSE_EXTENSIONS
        
                // Double (FloatLike)
        
        #if !T4INCLUDE__SUPPRESS_DOUBLE_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Double value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Double Parse (this string s, CultureInfo cultureInfo, Double defaultValue)
                {
                    Double value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Double Parse (this string s, Double defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Double value)
                {                                                  
                    return Double.TryParse (s ?? "", NumberStyles.Float, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_DOUBLE_PARSE_EXTENSIONS
        
                // Decimal (FloatLike)
        
        #if !T4INCLUDE__SUPPRESS_DECIMAL_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out Decimal value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static Decimal Parse (this string s, CultureInfo cultureInfo, Decimal defaultValue)
                {
                    Decimal value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static Decimal Parse (this string s, Decimal defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out Decimal value)
                {                                                  
                    return Decimal.TryParse (s ?? "", NumberStyles.Float, cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_DECIMAL_PARSE_EXTENSIONS
        
                // TimeSpan (TimeSpanLike)
        
        #if !T4INCLUDE__SUPPRESS_TIMESPAN_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out TimeSpan value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static TimeSpan Parse (this string s, CultureInfo cultureInfo, TimeSpan defaultValue)
                {
                    TimeSpan value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static TimeSpan Parse (this string s, TimeSpan defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out TimeSpan value)
                {                                                  
                    return TimeSpan.TryParse (s ?? "", cultureInfo, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_TIMESPAN_PARSE_EXTENSIONS
        
                // DateTime (DateTimeLike)
        
        #if !T4INCLUDE__SUPPRESS_DATETIME_PARSE_EXTENSIONS
        
                public static bool TryParse (this string s, out DateTime value)
                {
                    return s.TryParse (Config.DefaultCulture, out value);
                }
        
                public static DateTime Parse (this string s, CultureInfo cultureInfo, DateTime defaultValue)
                {
                    DateTime value;
        
                    return s.TryParse (cultureInfo, out value) ? value : defaultValue;
                }
        
                public static DateTime Parse (this string s, DateTime defaultValue)
                {
                    return s.Parse (Config.DefaultCulture, defaultValue);
                }
        
                public static bool TryParse (this string s, CultureInfo cultureInfo, out DateTime value)
                {                                                  
                    return DateTime.TryParse (s ?? "", cultureInfo, DateTimeStyles.AssumeLocal, out value);
                }
        
        #endif // T4INCLUDE__SUPPRESS_DATETIME_PARSE_EXTENSIONS
        
            }
        }
        
        
    }
    // @@@ END_INCLUDE: https://raw.github.com/mrange/T4Include/master/Extensions/ParseExtensions.cs
    // ############################################################################
    
    // ############################################################################
    // @@@ BEGIN_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Generated_Log.cs
    namespace SASBikes.Common
    {
        // ############################################################################
        // #                                                                          #
        // #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
        // #                                                                          #
        // # This means that any edits to the .cs file will be lost when its          #
        // # regenerated. Changes should instead be applied to the corresponding      #
        // # template file (.tt)                                                      #
        // ############################################################################
        
        
        
        
        
        
        namespace Source.Common
        {
            using System;
        
            partial class Log
            {
                public enum Level
                {
                    Success = 1000,
                    HighLight = 2000,
                    Info = 3000,
                    Warning = 10000,
                    Error = 20000,
                    Exception = 21000,
                }
        
                public static void Success (string format, params object[] args)
                {
                    LogMessage (Level.Success, format, args);
                }
                public static void HighLight (string format, params object[] args)
                {
                    LogMessage (Level.HighLight, format, args);
                }
                public static void Info (string format, params object[] args)
                {
                    LogMessage (Level.Info, format, args);
                }
                public static void Warning (string format, params object[] args)
                {
                    LogMessage (Level.Warning, format, args);
                }
                public static void Error (string format, params object[] args)
                {
                    LogMessage (Level.Error, format, args);
                }
                public static void Exception (string format, params object[] args)
                {
                    LogMessage (Level.Exception, format, args);
                }
        #if !NETFX_CORE && !SILVERLIGHT && !WINDOWS_PHONE
                static ConsoleColor GetLevelColor (Level level)
                {
                    switch (level)
                    {
                        case Level.Success:
                            return ConsoleColor.Green;
                        case Level.HighLight:
                            return ConsoleColor.White;
                        case Level.Info:
                            return ConsoleColor.Gray;
                        case Level.Warning:
                            return ConsoleColor.Yellow;
                        case Level.Error:
                            return ConsoleColor.Red;
                        case Level.Exception:
                            return ConsoleColor.Red;
                        default:
                            return ConsoleColor.Magenta;
                    }
                }
        #endif
                static string GetLevelMessage (Level level)
                {
                    switch (level)
                    {
                        case Level.Success:
                            return "SUCCESS  ";
                        case Level.HighLight:
                            return "HIGHLIGHT";
                        case Level.Info:
                            return "INFO     ";
                        case Level.Warning:
                            return "WARNING  ";
                        case Level.Error:
                            return "ERROR    ";
                        case Level.Exception:
                            return "EXCEPTION";
                        default:
                            return "UNKNOWN  ";
                    }
                }
        
            }
        }
        
    }
    // @@@ END_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Generated_Log.cs
    // ############################################################################
    
    // ############################################################################
    // @@@ BEGIN_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Array.cs
    namespace SASBikes.Common
    {
        // ----------------------------------------------------------------------------------------------
        // Copyright (c) M�rten R�nge.
        // ----------------------------------------------------------------------------------------------
        // This source code is subject to terms and conditions of the Microsoft Public License. A 
        // copy of the license can be found in the License.html file at the root of this distribution. 
        // If you cannot locate the  Microsoft Public License, please send an email to 
        // dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
        //  by the terms of the Microsoft Public License.
        // ----------------------------------------------------------------------------------------------
        // You must not remove this notice, or any other, from this software.
        // ----------------------------------------------------------------------------------------------
        
        namespace Source.Common
        {
            static class Array<T>
            {
                public static readonly T[] Empty = new T[0];
            }
        }
    }
    // @@@ END_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Array.cs
    // ############################################################################
    
    // ############################################################################
    // @@@ BEGIN_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Config.cs
    namespace SASBikes.Common
    {
        // ----------------------------------------------------------------------------------------------
        // Copyright (c) M�rten R�nge.
        // ----------------------------------------------------------------------------------------------
        // This source code is subject to terms and conditions of the Microsoft Public License. A 
        // copy of the license can be found in the License.html file at the root of this distribution. 
        // If you cannot locate the  Microsoft Public License, please send an email to 
        // dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
        //  by the terms of the Microsoft Public License.
        // ----------------------------------------------------------------------------------------------
        // You must not remove this notice, or any other, from this software.
        // ----------------------------------------------------------------------------------------------
        
        
        namespace Source.Common
        {
            using System.Globalization;
        
            sealed partial class InitConfig
            {
                public CultureInfo DefaultCulture = CultureInfo.InvariantCulture;
            }
        
            static partial class Config
            {
                static partial void Partial_Constructed(ref InitConfig initConfig);
        
                public readonly static CultureInfo DefaultCulture;
        
                static Config ()
                {
                    var initConfig = new InitConfig();
        
                    Partial_Constructed (ref initConfig);
        
                    initConfig = initConfig ?? new InitConfig();
        
                    DefaultCulture = initConfig.DefaultCulture;
                }
            }
        }
    }
    // @@@ END_INCLUDE: https://raw.github.com/mrange/T4Include/master/Common/Config.cs
    // ############################################################################
    
    // ############################################################################
    namespace SASBikes.Common.Include
    {
        static partial class MetaData
        {
            public const string RootPath        = @"https://raw.github.com/";
            public const string IncludeDate     = @"2013-03-02T10:39:28";
    
            public const string Include_0       = @"https://raw.github.com/mrange/T4Include/master/Common/Log.cs";
            public const string Include_1       = @"https://raw.github.com/mrange/T4Include/master/Extensions/BasicExtensions.cs";
            public const string Include_2       = @"https://raw.github.com/mrange/T4Include/master/Extensions/ParseExtensions.cs";
            public const string Include_3       = @"https://raw.github.com/mrange/T4Include/master/Common/Generated_Log.cs";
            public const string Include_4       = @"https://raw.github.com/mrange/T4Include/master/Common/Array.cs";
            public const string Include_5       = @"https://raw.github.com/mrange/T4Include/master/Common/Config.cs";
        }
    }
    // ############################################################################
    
    
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Include_T4Include.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Internal\Log.cs
namespace SASBikes.Common
{
    
    using System;
    using System.Collections.Concurrent;
    using SASBikes.Common.AppServices;
    using SASBikes.Common.DataModel;
    
    namespace SASBikes.Common.Source.Common
    {
        static partial class Log
        {
            readonly static ConcurrentQueue<string> s_errors = new ConcurrentQueue<string> ();
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
                        s_errors.Enqueue (message ?? "");
                        Services.App.Async_Invoke(AsyncGroup.Log_UpdateErrors, Log_UpdateErrors);
                        break;
                }
            }
    
            static void Log_UpdateErrors()
            {
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
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Internal\Log.cs
// ############################################################################

// ############################################################################
namespace SASBikes.Common.Include
{
    static partial class MetaData
    {
        public const string RootPath        = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.WP8.Common\..\SASBikes.Common";
        public const string IncludeDate     = @"2013-03-02T10:39:31";

        public const string Include_0       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppStateService.cs";
        public const string Include_1       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\Generated_Services.cs";
        public const string Include_2       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LocatorService.cs";
        public const string Include_3       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\StationsService.cs";
        public const string Include_4       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModel.cs";
        public const string Include_5       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelBase.cs";
        public const string Include_6       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelCollection.cs";
        public const string Include_7       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelContext.cs";
        public const string Include_8       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelSerializer.cs";
        public const string Include_9       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\Generated_DataModel.cs";
        public const string Include_10       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IDataModelEntity.cs";
        public const string Include_11       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IUnserializeErrorReporter.cs";
        public const string Include_12       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Extensions.cs";
        public const string Include_13       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\GeoCoordinate.cs";
        public const string Include_14       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Include_T4Include.cs";
        public const string Include_15       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Internal\Log.cs";
    }
}
// ############################################################################






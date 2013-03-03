



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
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\Generated_Services.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LocatorService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LogService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\StationsService.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\C.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Collections\ObservableDictionary.cs
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
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentDictionary.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentQueue.cs
// @@@ INCLUDING: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\Runner.cs
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
// ReSharper disable RedundantThisQualifier
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppService.cs
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Xml.Linq;
    using SASBikes.Common.Collections;
    using SASBikes.Common.DataModel;
    using SASBikes.Common.Source.Common;
    using SASBikes.Common.Source.Extensions;
    using SASBikes.Common.WindowsAdaptors;
    using Windows.Foundation;
    using Windows.Foundation.Collections;
    using Windows.UI.Core;
    
    namespace SASBikes.Common.AppServices
    {
        partial class StartServiceContext
        {
            public IRunner Runner;
        }
    
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
    
        public sealed class AppService : IService
        {
            public const string SampleData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
    <carto>
      <markers>
        <marker name=""LILLA BOMMEN"" number=""1"" address=""001_LILLA BOMMEN"" fullAddress=""001_LILLA BOMMEN  "" lat=""57.711547"" lng=""11.966574"" open=""1"" bonus=""0""/>
        <marker name=""LILLATORGET"" number=""2"" address=""002_LILLA TORGET"" fullAddress=""002_LILLA TORGET  "" lat=""57.705529"" lng=""11.962491"" open=""1"" bonus=""0""/>
        <marker name=""POSTGATAN"" number=""3"" address=""003_POSTGATAN"" fullAddress=""003_POSTGATAN  "" lat=""57.707476"" lng=""11.963926"" open=""1"" bonus=""0""/>
        <marker name=""STORA BADHUSGATAN"" number=""4"" address=""004_STORA BADHUSGATAN"" fullAddress=""004_STORA BADHUSGATAN  "" lat=""57.704819"" lng=""11.957907"" open=""1"" bonus=""0""/>
        <marker name=""VASAGATAN/HEDEN"" number=""5"" address=""005_VASAGATAN/HEDEN"" fullAddress=""005_VASAGATAN/HEDEN  "" lat=""57.701419"" lng=""11.9766"" open=""0"" bonus=""0""/>
        <marker name=""SLUSSPLATSEN"" number=""6"" address=""006_SLUSSPLATSEN"" fullAddress=""006_SLUSSPLATSEN  "" lat=""57.707316"" lng=""11.973448"" open=""1"" bonus=""0""/>
        <marker name=""ESPERANTOPLATSEN"" number=""8"" address=""008_ESPERANTOPLATSEN"" fullAddress=""008_ESPERANTOPLATSEN  "" lat=""57.702793"" lng=""11.955461"" open=""1"" bonus=""0""/>
        <marker name=""KANALTORGET"" number=""9"" address=""009_KANALTORGET"" fullAddress=""009_KANALTORGET  "" lat=""57.710396"" lng=""11.966421"" open=""1"" bonus=""0""/>
        <marker name=""VALAND"" number=""10"" address=""010_VALAND"" fullAddress=""010_VALAND  "" lat=""57.700324"" lng=""11.973429"" open=""1"" bonus=""0""/>
        <marker name=""STORAN"" number=""11"" address=""011_STORAN"" fullAddress=""011_STORAN  "" lat=""57.702482"" lng=""11.971552"" open=""1"" bonus=""0""/>
        <marker name=""GRÖNSAKSTORGET"" number=""12"" address=""012_GRÖNSAKSTORGET"" fullAddress=""012_GRÖNSAKSTORGET  "" lat=""57.702459"" lng=""11.965179"" open=""1"" bonus=""0""/>
        <marker name=""GUSTAF ADOLFS TORG"" number=""13"" address=""013_GUSTAF ADOLFS TORG"" fullAddress=""013_GUSTAF ADOLFS TORG  "" lat=""57.707233"" lng=""11.967502"" open=""1"" bonus=""0""/>
        <marker name=""KUNGSTORGET"" number=""17"" address=""017_KUNGSTORGET"" fullAddress=""017_KUNGSTORGET  "" lat=""57.702542"" lng=""11.968875"" open=""1"" bonus=""0""/>
        <marker name=""BASTIONSPLATSEN"" number=""21"" address=""021_BASTIONSPLATSEN"" fullAddress=""021_BASTIONSPLATSEN  "" lat=""57.704263"" lng=""11.972077"" open=""1"" bonus=""0""/>
        <marker name=""KRISTINELUNDSGATAN"" number=""14"" address=""014_KRISTINELUNDSGATAN"" fullAddress=""014_KRISTINELUNDSGATAN  "" lat=""57.699939"" lng=""11.976176"" open=""1"" bonus=""0""/>
        <marker name=""HEDEN/STENSTUREGATAN"" number=""15"" address=""015_HEDEN/STENSTUREGATAN"" fullAddress=""015_HEDEN/STENSTUREGATAN  "" lat=""57.702955"" lng=""11.98021"" open=""1"" bonus=""0""/>
        <marker name=""BOHUSGATAN"" number=""16"" address=""016_BOHUSGATAN"" fullAddress=""016_BOHUSGATAN  "" lat=""57.70452"" lng=""11.984298"" open=""1"" bonus=""0""/>
        <marker name=""ROSENLUNDSPLATSEN"" number=""18"" address=""018_ROSENLUNDSPLATSEN"" fullAddress=""018_ROSENLUNDSPLATSEN  "" lat=""57.700685"" lng=""11.960571"" open=""1"" bonus=""0""/>
        <marker name=""OLOF PALMES PLATS"" number=""19"" address=""019_OLOF PALMES PLATS"" fullAddress=""019_OLOF PALMES PLATS  "" lat=""57.700662"" lng=""11.95364"" open=""1"" bonus=""0""/>
        <marker name=""NILS-ERICSSON TERMINALEN"" number=""20"" address=""020_NILS-ERICSSON TERMINALEN"" fullAddress=""020_NILS-ERICSSON TERMINALEN  "" lat=""57.709752"" lng=""11.970787"" open=""1"" bonus=""0""/>
        <marker name=""DÄMMEPLATSEN"" number=""22"" address=""022_Dämmeplatsen"" fullAddress=""022_Dämmeplatsen  "" lat=""57.707576"" lng=""11.989893"" open=""1"" bonus=""0""/>
        <marker name=""SCANDINAVIUM"" number=""25"" address=""025_Scandinavium"" fullAddress=""025_Scandinavium  "" lat=""57.700094"" lng=""11.987908"" open=""1"" bonus=""0""/>
        <marker name=""LISEBERG STATION"" number=""27"" address=""027_Liseberg station"" fullAddress=""027_Liseberg station  "" lat=""57.697923"" lng=""11.995107"" open=""1"" bonus=""0""/>
        <marker name=""GÅRDATORGET"" number=""24"" address=""024_Gårdatorget"" fullAddress=""024_Gårdatorget  "" lat=""57.704675"" lng=""11.992441"" open=""1"" bonus=""0""/>
        <marker name=""KORSVÄGEN /SÖDRA VÄGEN"" number=""28"" address=""028_Korsvägen 1"" fullAddress=""028_Korsvägen 1  "" lat=""57.696916"" lng=""11.985703"" open=""1"" bonus=""0""/>
        <marker name=""KORSVÄGEN"" number=""29"" address=""029_Korsvägen 2"" fullAddress=""029_Korsvägen 2  "" lat=""57.696555"" lng=""11.987833"" open=""1"" bonus=""0""/>
        <marker name=""SKÅNEGATAN"" number=""23"" address=""023_Skånegatan"" fullAddress=""023_Skånegatan  "" lat=""57.706096"" lng=""11.984646"" open=""1"" bonus=""0""/>
        <marker name=""BERGAKUNGEN"" number=""30"" address=""030_Bergakungen"" fullAddress=""030_Bergakungen  "" lat=""57.702135"" lng=""11.985035"" open=""1"" bonus=""0""/>
        <marker name=""HEDEN SYD"" number=""26"" address=""026_Heden syd"" fullAddress=""026_Heden syd  "" lat=""57.699941"" lng=""11.979867"" open=""0"" bonus=""0""/>
        <marker name=""VASAGATAN/SCHILLERSKA"" number=""38"" address=""038_Vasagatan/Schillerska"" fullAddress=""038_Vasagatan/Schillerska  "" lat=""57.698265"" lng=""11.967115"" open=""1"" bonus=""0""/>
        <marker name=""PACKHUSPLATSEN"" number=""43"" address=""043_PACKHUSPLATSEN"" fullAddress=""043_PACKHUSPLATSEN  "" lat=""57.706767"" lng=""11.959101"" open=""1"" bonus=""0""/>
        <marker name=""ALSTRÖMER/FRIGGAGATAN"" number=""45"" address="""" fullAddress=""  "" lat=""57.71119"" lng=""11.98798"" open=""1"" bonus=""0""/>
        <marker name=""SKANSTORGET"" number=""35"" address=""035_Skanstorget"" fullAddress=""035_Skanstorget  "" lat=""57.695909"" lng=""11.958548"" open=""1"" bonus=""0""/>
        <marker name=""KASTELLGATAN"" number=""34"" address=""034_Kastellgatan"" fullAddress=""034_Kastellgatan  "" lat=""57.693029"" lng=""11.955692"" open=""1"" bonus=""0""/>
        <marker name=""BRUNNSGATAN"" number=""32"" address=""032_Brunnsgatan"" fullAddress=""032_Brunnsgatan  "" lat=""57.693517"" lng=""11.958559"" open=""1"" bonus=""0""/>
        <marker name=""MOLINSGATAN"" number=""37"" address=""037_Molinsgatan"" fullAddress=""037_Molinsgatan  "" lat=""57.69691"" lng=""11.97234"" open=""1"" bonus=""0""/>
        <marker name=""ÅVÄGEN"" number=""31"" address=""031_Åvägen"" fullAddress=""031_Åvägen  "" lat=""57.701249"" lng=""11.992191"" open=""1"" bonus=""0""/>
        <marker name=""FRIGÅNGSGATAN"" number=""39"" address=""039_Frigångsgatan"" fullAddress=""039_Frigångsgatan  "" lat=""57.697821"" lng=""11.952905"" open=""1"" bonus=""0""/>
        <marker name=""SLOTTSKOGEN/PLIKTA"" number=""41"" address=""041_Slottskogen/Plikta"" fullAddress=""041_Slottskogen/Plikta  "" lat=""57.689983"" lng=""11.947047"" open=""1"" bonus=""0""/>
        <marker name=""KAPELLPLATSEN"" number=""46"" address="""" fullAddress=""  "" lat=""57.69358"" lng=""11.97199"" open=""1"" bonus=""0""/>
        <marker name=""DROTTNINGTORGET"" number=""42"" address=""042_Drottningtorget"" fullAddress=""042_Drottningtorget  "" lat=""57.708074"" lng=""11.972737"" open=""1"" bonus=""0""/>
        <marker name=""BERZELIIGATAN"" number=""47"" address="""" fullAddress=""  "" lat=""57.69794"" lng=""11.98096"" open=""1"" bonus=""0""/>
        <marker name=""CARLANDERSPLATSEN"" number=""48"" address="""" fullAddress=""  "" lat=""57.69312"" lng=""11.98661"" open=""1"" bonus=""0""/>
        <marker name=""KAPONJÄRSGATAN"" number=""49"" address="""" fullAddress=""  "" lat=""57.69875"" lng=""11.95649"" open=""1"" bonus=""0""/>
        <marker name=""KUNGSGATAN/TELEKASERN"" number=""50"" address="""" fullAddress=""  "" lat=""57.70337"" lng=""11.96012"" open=""1"" bonus=""0""/>
        <marker name=""FÖRENINGSGATAN"" number=""33"" address=""033_Föreningsgatan"" fullAddress=""033_Föreningsgatan  "" lat=""57.694554"" lng=""11.961987"" open=""1"" bonus=""0""/>
        <marker name=""ENGELBREKTSGATAN"" number=""44"" address="""" fullAddress=""  "" lat=""57.69841"" lng=""11.97694"" open=""1"" bonus=""0""/>
        <marker name=""HAGAPARKEN"" number=""36"" address=""036_Hagaparken"" fullAddress=""036_Hagaparken  "" lat=""57.697984"" lng=""11.962405"" open=""1"" bonus=""0""/>
        <marker name=""LINNÉPLATSEN"" number=""40"" address=""040_Linnéplatsen"" fullAddress=""040_Linnéplatsen  "" lat=""57.690278"" lng=""11.95165"" open=""1"" bonus=""0""/>
        <marker name=""HAGABION"" number=""51"" address="""" fullAddress=""  "" lat=""57.696837"" lng=""11.951009"" open=""1"" bonus=""0""/>
        <marker name=""POSTHUSET/ÅKAREPLATSEN"" number=""7"" address="""" fullAddress=""  "" lat=""57.707647"" lng=""11.976117"" open=""1"" bonus=""0""/>
        <marker name=""GIBRALTARG/EKLANDAG"" number=""52"" address=""GIBRALTARGATAN/EKLANDAGATAN"" fullAddress=""GIBRALTARGATAN/EKLANDAGATAN  "" lat=""57.68542"" lng=""11.98322"" open=""1"" bonus=""0""/>
        <marker name=""MOLINSGATAN/LÄRAREGATAN"" number=""53"" address="""" fullAddress=""  "" lat=""57.69418"" lng=""11.97451"" open=""1"" bonus=""0""/>
        <marker name=""EKLANDA/UTLANDAGATAN"" number=""54"" address="""" fullAddress=""  "" lat=""57.69032"" lng=""11.98854"" open=""1"" bonus=""0""/>
      </markers>
      <arrondissements>
        <arrondissement number=""0"" minLat=""57.68542"" minLng=""11.947047"" maxLat=""57.711547"" maxLng=""11.995107""/>
      </arrondissements>
    </carto>
    ";
    
            public readonly IObservableMap<string, object> ViewModel = new ObservableDictionary<string, object>(); 
    
            public State State
            {
                get
                {
                    object state;
                    return ViewModel.TryGetValue(C.ViewModel.ApplicationState, out state)
                        ?   state as State
                        :   default(State)
                        ;
                }
                set
                {
                    ViewModel[C.ViewModel.ApplicationState] = value;
                }
            }
            public IRunner Runner;
    
            enum DispatcherState
            {
                Idle                ,
                Triggered           ,
                Dispatching         ,
            }
    
            sealed class DispatcherStateManager
            {
                int m_state;
    
                public bool Edge(DispatcherState from, DispatcherState to)
                {
                    var t = (int) to;
                    var f = (int) from;
                    return Interlocked.CompareExchange (ref m_state, t, f) == f;
                }
            }
    
            readonly DispatcherStateManager m_dispatcherState = new DispatcherStateManager (); 
            readonly IConcurrentQueue<Tuple<AsyncGroup, Action>> m_dispatchedAsyncCalls = new ConcurrentQueue<Tuple<AsyncGroup, Action>>();
    
            public void Start(StartServiceContext context)
            {
                Runner  = context.Runner        ;
                if (State == null)
                {
                    State= CreateEmptyState()            ;
                }
            }
    
            public void Stop(StopServiceContext context)
            {
                Runner  = null                              ;
            }
    
            public void Async_Invoke(AsyncGroup group, Action action)
            {
                if (action == null)
                {
                    return;
                }
    
                m_dispatchedAsyncCalls.Enqueue(Tuple.Create (group, action));
    
                var dispatcher = Runner;
    
                if (dispatcher == null)
                {
                    return;
                }
    
                StartDispatcher(dispatcher);
            }
    
            void StartDispatcher(IRunner runner)
            {
                if (m_dispatcherState.Edge(DispatcherState.Idle, DispatcherState.Triggered))
                {
                    runner.RunOnApplicationIdle(RunIdle_DispatchActions);
                }
            }
    
            void RunIdle_DispatchActions()
            {
                if (!m_dispatcherState.Edge(DispatcherState.Triggered, DispatcherState.Dispatching))
                {
                    return;    
                }
    
                try
                {
                    var groupedActions = new Dictionary<AsyncGroup, Tuple<Action, int>> (); 
    
                    for(;;)
                    {
                        Tuple<AsyncGroup, Action> value;
                        groupedActions.Clear();
                        while (m_dispatchedAsyncCalls.TryDequeue(out value))
                        {
                            if (value.Item2 != null)
                            {
                                groupedActions[value.Item1] = Tuple.Create(value.Item2, groupedActions.Count);
                            }
                        }
    
                        if (groupedActions.Count == 0)
                        {
                            return;
                        }
    
                        foreach (var kv in groupedActions.OrderBy(kv => kv.Value.Item2))
                        {
                            try
                            {
                                kv.Value.Item1();
                            }
                            catch (Exception exc)
                            {
                                Log.Exception ("Failed to dispatch async action {0}: {1}", kv.Key, exc);                
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    Log.Exception ("Failed to dispatch async actions: {0}", exc);                
                }
                finally
                {
                    m_dispatcherState.Edge(DispatcherState.Dispatching, DispatcherState.Idle);
    
                    var dispatcher = Runner;
                    if (dispatcher != null && m_dispatchedAsyncCalls.Count > 0)
                    {
                        StartDispatcher(dispatcher);
                    }
    
                }
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
    
            State CreateEmptyState()
            {
                var context = new DataModelContext();
                var state = new State(context)
                                {
                                    State_Lo            = 11.973429     ,
                                    State_La            = 57.700324     ,
                                    State_StationName   = "VALAND"      ,
                                    State_ZoomLevel     = 18            ,
                                };
    
                var stations = CreateStations(state.Context, SampleData).ToArray();
    
                foreach (var station in stations)
                {
                    state.State_Stations.Add(station);
                }
    
                return state;
            }
    
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppService.cs
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
        public partial class StartServiceContext
        {
        }
    
        public partial class StopServiceContext
        {
        }
    
        public partial interface IService
        {
            void Start (StartServiceContext context);
            void Stop (StopServiceContext context);
        }
    
        public static partial class Services
        {
            public static readonly AppService App = new AppService()      ;
            public static readonly LogService Log = new LogService()      ;
            public static readonly LocatorService Locator = new LocatorService()      ;
            public static readonly StationsService Stations = new StationsService()      ;
    
            public static void Start(StartServiceContext context)
            {
                var state = SetState(States.Started);
                if (state == States.Stopped)
                {
                    StartService (App, context);
                    StartService (Log, context);
                    StartService (Locator, context);
                    StartService (Stations, context);
                }
            }
    
            public static void Stop(StopServiceContext context)
            {
                var state = SetState(States.Stopped);
                if (state == States.Started)
                {
                    StopService (Stations, context);
                    StopService (Locator, context);
                    StopService (Log, context);
                    StopService (App, context);
                }
            }
    
            static void StopService(this IService service, StopServiceContext context)
            {
                if (service != null)
                {
                    try
                    {
                        service.Stop(context);
                    }
                    catch (Exception exc)
                    {
                        Source.Common.Log.Exception ("Failed to stop service {0}: {1}", service.GetType().Name, exc);
                    }
                }
                
            }
    
            static void StartService(this IService service, StartServiceContext context)
            {
                if (service != null)
                {
                    try
                    {
                        service.Start(context);
                    }
                    catch (Exception exc)
                    {
                        Source.Common.Log.Exception ("Failed to start service {0}: {1}", service.GetType().Name, exc);
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
        public sealed class LocatorService : IService
        {
            Geolocator m_locator;
            Geoposition m_lastKnownPosition;
    
            public void Start(StartServiceContext context)
            {
                m_locator = new Geolocator
                                {
                                    MovementThreshold = C.Default.MovementThreshold
                                };
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
    
            public void Stop(StopServiceContext context)
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
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LogService.cs
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
    using SASBikes.Common.DataModel;
    using SASBikes.Common.WindowsAdaptors;
    
    namespace SASBikes.Common.AppServices
    {
        public sealed class LogService : IService
        {
            readonly IConcurrentQueue<string> s_errors = new ConcurrentQueue<string> ();
            bool m_isRunning;
    
            public void Start(StartServiceContext context)
            {
                m_isRunning = true;
                Services.App.Async_Invoke(AsyncGroup.Log_UpdateErrors, Log_UpdateErrors);
            }
    
            public void Stop(StopServiceContext context)
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
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LogService.cs
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
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using SASBikes.Common.Source.Common;
    using SASBikes.Common.Source.Extensions;
    
    namespace SASBikes.Common.AppServices
    {
        public sealed class StationsService : IService
        {
            const int Delay_InitialUpdateStations   =        5   *1000   ;
            const int Delay_UpdateStations          = 5     *60  *1000   ;
    
            CancellationTokenSource m_source;
            Task m_updateTask;
            string m_lastXmlData;
    
            public void Start(StartServiceContext context)
            {
                m_source = new CancellationTokenSource();
                var token = m_source.Token;
    
                m_updateTask = Task
                        .Delay(Delay_InitialUpdateStations, token)
                        .ContinueWith(t => UpdateStations(token), token)
                        ;
            }
    
            public void Stop(StopServiceContext context)
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
                    var webRequest = WebRequest.CreateHttp(@"http://www.goteborgbikes.se/index.php/service/carto");
                    webRequest.Method = "GET";
                    Func<AsyncCallback, object, IAsyncResult> beginMethod = webRequest.BeginGetResponse;
                    Func<IAsyncResult, WebResponse> endMethod = webRequest.EndGetResponse;
    
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
    
                    var task = Task.Factory.FromAsync(
                        beginMethod, 
                        endMethod,
                        null
                        );
    
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
    
                    var webResponse = task
                        .Result
                        ;
    
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
    
                    var contentType = webResponse.ContentType;
                    if ("text/xml; charset=utf-8".Equals(contentType, StringComparison.OrdinalIgnoreCase))
                    {
                        using (var stream = webResponse.GetResponseStream())
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            if (cancellationToken.IsCancellationRequested)
                            {
                                return;
                            }
    
                            var xmlData = reader.ReadToEnd();
                            if (!xmlData.IsNullOrWhiteSpace())
                            {
                                m_lastXmlData = xmlData;
                                Services.App.Async_Invoke (AsyncGroup.StationsService_UpdateStations, StationsService_UpdateStations);
                            }
                        }
                    }
                    else
                    {
                        Log.Error ("Invalid content-type returned from station service: {0}", contentType);
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
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\C.cs
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
    
    
    namespace SASBikes.Common
    {
        public static class C
        {
            public static class ViewModel
            {
                public const string ApplicationState = "ViewModel_ApplicationState"  ;
            }
    
            public static class Default
            {
                public const double   MovementThreshold     = 2.0   ;
                public const double   My_Lo    = 11.973429  ;
                public const double   My_La    = 57.700324  ;
                public const double   View_Lo  = 11.973429  ;
                public const double   View_La  = 57.700324  ;
                public const double   View_Zoom= 18         ;
            }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\C.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Collections\ObservableDictionary.cs
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
    using System.Collections.Generic;
    using System.Linq;
    using Windows.Foundation.Collections;
    
    
    namespace SASBikes.Common.Collections
    {
        public sealed partial class ObservableDictionary<K, V> : IObservableMap<K, V>
        {
            private class ObservableDictionaryChangedEventArgs : IMapChangedEventArgs<K>
            {
                public ObservableDictionaryChangedEventArgs(CollectionChange change, K key)
                {
                    this.CollectionChange = change;
                    this.Key = key;
                }
    
                public CollectionChange CollectionChange { get; private set; }
                public K Key { get; private set; }
            }
    
            private Dictionary<K, V> _dictionary = new Dictionary<K, V>();
            public event MapChangedEventHandler<K, V> MapChanged;
    
            private void InvokeMapChanged(CollectionChange change, K key)
            {
                var eventHandler = MapChanged;
                if (eventHandler != null)
                {
                    eventHandler(this, new ObservableDictionaryChangedEventArgs(change, key));
                }
            }
    
            public void Add(K key, V value)
            {
                this._dictionary.Add(key, value);
                this.InvokeMapChanged(CollectionChange.ItemInserted, key);
            }
    
            public void Add(KeyValuePair<K, V> item)
            {
                this.Add(item.Key, item.Value);
            }
    
            public bool Remove(K key)
            {
                if (this._dictionary.Remove(key))
                {
                    this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                    return true;
                }
                return false;
            }
    
            public bool Remove(KeyValuePair<K, V> item)
            {
                V currentValue;
                if (this._dictionary.TryGetValue(item.Key, out currentValue) &&
                    Object.Equals(item.Value, currentValue) && this._dictionary.Remove(item.Key))
                {
                    this.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key);
                    return true;
                }
                return false;
            }
    
            public V this[K key]
            {
                get
                {
                    return this._dictionary[key];
                }
                set
                {
                    this._dictionary[key] = value;
                    this.InvokeMapChanged(CollectionChange.ItemChanged, key);
                }
            }
    
            public void Clear()
            {
                var priorKeys = this._dictionary.Keys.ToArray();
                this._dictionary.Clear();
                foreach (var key in priorKeys)
                {
                    this.InvokeMapChanged(CollectionChange.ItemRemoved, key);
                }
            }
    
            public ICollection<K> Keys
            {
                get { return this._dictionary.Keys; }
            }
    
            public bool ContainsKey(K key)
            {
                return this._dictionary.ContainsKey(key);
            }
    
            public bool TryGetValue(K key, out V value)
            {
                return this._dictionary.TryGetValue(key, out value);
            }
    
            public ICollection<V> Values
            {
                get { return this._dictionary.Values; }
            }
    
            public bool Contains(KeyValuePair<K, V> item)
            {
                return this._dictionary.Contains(item);
            }
    
            public int Count
            {
                get { return this._dictionary.Count; }
            }
    
            public bool IsReadOnly
            {
                get { return false; }
            }
    
            public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
            {
                return this._dictionary.GetEnumerator();
            }
    
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this._dictionary.GetEnumerator();
            }
    
            public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
            {
                int arraySize = array.Length;
                foreach (var pair in this._dictionary)
                {
                    if (arrayIndex >= arraySize) break;
                    array[arrayIndex++] = pair;
                }
            }
        }
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Collections\ObservableDictionary.cs
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
    using SASBikes.Common.AppServices;
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
                if (state == null)
                {
                    return "";
                }
                var doc = new XDocument(state.Serialize(RootName));
    
                using (var sw = new StringWriter())
                {
                    doc.Save(sw);
                    return sw.ToString();
                }
            }
    
            public static State UnserializeFromString(this string value)
            {
                if (value.IsNullOrWhiteSpace())
                {
                    value = AppService.SampleData;
                }
    
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
                _State_ZoomLevel = C.Default.View_Zoom   ;
                _State_Lo = C.Default.View_Lo   ;
                _State_La = C.Default.View_La   ;
                _State_MyLo = C.Default.My_Lo   ;
                _State_MyLa = C.Default.My_La   ;
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
                                var value = C.Default.View_Zoom;
    
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
                                var value = C.Default.View_Lo;
    
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
                                var value = C.Default.View_La;
    
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
                                var value = C.Default.My_Lo;
    
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
                                var value = C.Default.My_La;
    
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using SASBikes.Common.DataModel;
    
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
    
            public static IEnumerable<Station> NearestOpenStations (this IEnumerable<Station> stations)
            {
                if (stations == null)
                {
                    return new Station[0];
                }
    
                return stations
                    .Where (s => s.Station_IsOpen)
                    .OrderBy (s => s.Station_Distance)
                    ;
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
            public const string IncludeDate     = @"2013-03-03T19:40:25";
    
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
    
    using SASBikes.Common.AppServices;
    
    namespace SASBikes.Common.Source.Common
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
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Internal\Log.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentDictionary.cs
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
    
    namespace SASBikes.Common.WindowsAdaptors
    {
        partial interface IConcurrentDictionary<TKey, TValue>
        {
            bool TryAdd(TKey key, TValue value);        
            bool TryRemove(TKey key, out TValue value);
            TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
            void Clear ();
        }
    
    #if SILVERLIGHT || WINDOWS_PHONE
        sealed partial class ConcurrentDictionary<TKey, TValue> : IConcurrentDictionary<TKey, TValue>
        {
            readonly System.Collections.Generic.Dictionary<TKey, TValue> m_dictionary = new System.Collections.Generic.Dictionary<TKey, TValue> ();         
            public bool TryAdd(TKey key, TValue value)
            {
                lock (m_dictionary)
                {
                    if (!m_dictionary.ContainsKey(key))
                    {
                        return false;
                    }
    
                    m_dictionary[key] = value;                
    
                    return true;
                }
            }
    
            public bool TryRemove(TKey key, out TValue value)
            {
                lock (m_dictionary)
                {
                    if (!m_dictionary.TryGetValue(key, out value))
                    {
                        return false;
                    }
    
                    m_dictionary.Remove(key);
    
                    return true;
                }
            }
    
            public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
            {
                lock (m_dictionary)
                {
                    TValue value;
                    if (m_dictionary.TryGetValue (key, out value))
                    {
                        var newValue = updateValueFactory(key, value);
                        m_dictionary[key] = newValue;
                        return newValue;
                    }
                    else
                    {
                        m_dictionary.Add (key, addValue);
                        return addValue;
                    }
                }
            }
    
            public void Clear()
            {
                lock (m_dictionary)
                {
                    m_dictionary.Clear();
                }
            }
        }
    #else
        sealed partial class ConcurrentDictionary<TKey, TValue> : IConcurrentDictionary<TKey, TValue>
        {
            readonly System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue> m_dictionary = new System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue> ();         
            public bool TryAdd(TKey key, TValue value)
            {
                return m_dictionary.TryAdd(key, value);
            }
    
            public bool TryRemove(TKey key, out TValue value)
            {
                return m_dictionary.TryRemove(key, out value);
            }
    
            public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
            {
                return m_dictionary.AddOrUpdate(key, addValue, updateValueFactory);
            }
    
            public void Clear()
            {
                m_dictionary.Clear();
            }
        }
    #endif
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentDictionary.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentQueue.cs
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
    
    namespace SASBikes.Common.WindowsAdaptors
    {
        partial interface IConcurrentQueue<TValue>
        {
            int Count { get; }
            void Enqueue(TValue value);
            bool TryDequeue(out TValue value);
        }
    
    #if SILVERLIGHT || WINDOWS_PHONE
        sealed partial class ConcurrentQueue<TValue> : IConcurrentQueue<TValue>
        {
            readonly System.Collections.Generic.Queue<TValue> m_queue = new System.Collections.Generic.Queue<TValue>();
    
    
            public int Count
            {
                get
                {
                    lock (m_queue)
                    {
                        return m_queue.Count;
                    }
                }
            }
    
            public void Enqueue(TValue value)
            {
                lock (m_queue)
                {
                    m_queue.Enqueue(value);
                }
            }
    
            public bool TryDequeue(out TValue value)
            {
                lock (m_queue)
                {
                    if (m_queue.Count > 0)
                    {
                        value = m_queue.Dequeue();
                        return true;
                    }
                    else
                    {
                        value = default(TValue);
                        return false;
    
                    }
                }
            }
        }
    #else
        sealed partial class ConcurrentQueue<TValue> : IConcurrentQueue<TValue>
        {
            readonly System.Collections.Concurrent.ConcurrentQueue<TValue> m_queue = new System.Collections.Concurrent.ConcurrentQueue<TValue> ();
    
    
            public int Count { get { return m_queue.Count; }}
    
            public void Enqueue(TValue value)
            {
                m_queue.Enqueue(value);
            }
    
            public bool TryDequeue(out TValue value)
            {
                return m_queue.TryDequeue(out value);
            }
        }
    #endif
    }
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentQueue.cs
// ############################################################################

// ############################################################################
// @@@ BEGIN_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\Runner.cs
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
}
// @@@ END_INCLUDE: C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\Runner.cs
// ############################################################################

// ############################################################################
namespace SASBikes.Common.Include
{
    static partial class MetaData
    {
        public const string RootPath        = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.WP8.Common\..\SASBikes.Common";
        public const string IncludeDate     = @"2013-03-03T21:47:03";

        public const string Include_0       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\AppService.cs";
        public const string Include_1       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\Generated_Services.cs";
        public const string Include_2       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LocatorService.cs";
        public const string Include_3       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\LogService.cs";
        public const string Include_4       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\AppServices\StationsService.cs";
        public const string Include_5       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\C.cs";
        public const string Include_6       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Collections\ObservableDictionary.cs";
        public const string Include_7       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModel.cs";
        public const string Include_8       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelBase.cs";
        public const string Include_9       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelCollection.cs";
        public const string Include_10       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelContext.cs";
        public const string Include_11       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\DataModelSerializer.cs";
        public const string Include_12       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\Generated_DataModel.cs";
        public const string Include_13       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IDataModelEntity.cs";
        public const string Include_14       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\DataModel\IUnserializeErrorReporter.cs";
        public const string Include_15       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Extensions.cs";
        public const string Include_16       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\GeoCoordinate.cs";
        public const string Include_17       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Include_T4Include.cs";
        public const string Include_18       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\Internal\Log.cs";
        public const string Include_19       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentDictionary.cs";
        public const string Include_20       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\ConcurrentQueue.cs";
        public const string Include_21       = @"C:\temp\GitHub\bikes\SASBikes\SASBikes.Common\WindowsAdaptors\Runner.cs";
    }
}
// ############################################################################






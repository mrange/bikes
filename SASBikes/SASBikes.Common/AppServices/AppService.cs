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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SASBikes.Common.DataModel;
using SASBikes.Common.Source.Common;
using SASBikes.Common.Source.Extensions;
using SASBikes.Common.WindowsAdaptors;
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

    public sealed class AppService : IService
    {
        const string SampleData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
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

        public State State              ;
        public CoreWindow     Window    ;
        public CoreDispatcher Dispatcher;

        readonly IConcurrentDictionary<AsyncGroup, bool> m_dispatchedAsyncCalls = new ConcurrentDictionary<AsyncGroup, bool>();

        public void Start()
        {
            Window      = CoreWindow.GetForCurrentThread()  ;
            Dispatcher  = Window.Dispatcher                 ;
            if (State == null)
            {
                State       = CreateEmptyState()            ;
            }
            m_dispatchedAsyncCalls.Clear()                  ;
        }

        public void Stop()
        {
            m_dispatchedAsyncCalls.Clear()                  ;
            Dispatcher  = null                              ;
            Window      = null                              ;
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
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

    sealed class AppService : IService
    {
        public State State;
        public CoreDispatcher Dispatcher;

        readonly IConcurrentDictionary<AsyncGroup, bool> m_dispatchedAsyncCalls = new ConcurrentDictionary<AsyncGroup, bool>();

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
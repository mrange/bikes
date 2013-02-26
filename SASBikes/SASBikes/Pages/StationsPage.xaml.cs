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
using SASBikes.Common;
using SASBikes.DataModel;
using SASBikes.Source.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace SASBikes.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class StationsPage
    {
        public StationsPage()
        {
            InitializeComponent();

            App.Value.Dispatcher = Dispatcher;

            DefaultViewModel[C.ViewModel_ApplicationState] = App.Value.AppState;
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        void Click_FindNearestBike(object sender, RoutedEventArgs e)
        {
            Log.Error("TODO: Remove this");

            var appState = App.Value.AppState;

            var closestDistance = double.MaxValue;
            Station closestStation = null;

            for (var index = 0; index < appState.State_Stations.Count; index++)
            {
                var station = appState.State_Stations[index];
                if (!station.Station_IsOpen)
                {
                    continue;
                }

                var distance = station.Station_Distance;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestStation = station;
                }
            }

            if (closestStation != null)
            {
                appState.State_La = closestStation.Station_La;
                appState.State_Lo = closestStation.Station_Lo;
                appState.State_ZoomLevel = 20;
            }
        }
    }
}

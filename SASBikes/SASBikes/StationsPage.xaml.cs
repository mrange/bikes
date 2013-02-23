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
using Bing.Maps;
using SASBikes.Common;
using SASBikes.DataModel;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace SASBikes
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

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        void Click_ZoomOnMe(object sender, RoutedEventArgs e)
        {
            var appState = App.Value.AppState;
            appState.State_La = appState.State_MyLa;
            appState.State_Lo = appState.State_MyLo;
            appState.State_ZoomLevel = 20;
        }

        void Click_FindNearestBike(object sender, RoutedEventArgs e)
        {
            var appState = App.Value.AppState;
            var me = new Location(appState.State_La, appState.State_Lo);

            double closestDistance = double.MaxValue;
            Station closestStation = null;

            for (int index = 0; index < appState.State_Stations.Count; index++)
            {
                var station = appState.State_Stations[index];

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

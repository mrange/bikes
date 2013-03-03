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
using SASBikes.Common;
using SASBikes.Common.AppServices;
using SASBikes.Common.DataModel;
using SASBikes.Source.Common;
using Windows.UI.Xaml;

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

            DefaultViewModel = Services.App.ViewModel;
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        void Click_FindNearestBike(object sender, RoutedEventArgs e)
        {
            var appState = Services.App.State;

            var nearestStation = appState.State_Stations.NearestOpenStations ().FirstOrDefault ();

            if (nearestStation != null)
            {
                appState.State_La = nearestStation.Station_La;
                appState.State_Lo = nearestStation.Station_Lo;
                appState.State_ZoomLevel = 20;
            }
        }
    }
}

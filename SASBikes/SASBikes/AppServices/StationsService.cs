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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SASBikes.Source.Common;
using SASBikes.Source.Extensions;

namespace SASBikes.AppServices
{
    sealed class StationsService : IService
    {
        const int Delay_InitialUpdateStations   =        10  *1000   ;
        const int Delay_UpdateStations          = 5     *60  *1000   ;

        CancellationTokenSource m_source;
        Task m_updateTask;

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
                    var xmlData = httpClient.GetStringAsync(@"http://www.goteborgbikes.se/index.php/service/carto")
                        .Result
                        ?? ""
                        ;

                    if (!xmlData.IsNullOrWhiteSpace())
                    {
                        App.Value.Async_Invoke (App.AsyncGroup.StationsService_UpdateStations, () => StationsService_UpdateStations (xmlData));
                    }
                }
            }
            catch (Exception exc)
            {
                Log.Exception ("Failed to get station data: {0}", exc);
            }

            m_updateTask =
                Task
                    .Delay(Delay_UpdateStations, cancellationToken)
                    .ContinueWith(t => UpdateStations(cancellationToken), cancellationToken)
                    ;
        }

        void StationsService_UpdateStations(string xmlData)
        {
            App.Value.UpdateStations(xmlData);
        }
    }
}
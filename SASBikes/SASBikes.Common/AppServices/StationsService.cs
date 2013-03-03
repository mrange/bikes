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
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

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SASBikes.AppServices
{
    sealed class UpdateStationsService : IService
    {
        CancellationTokenSource m_source;
        Task m_updateTask;

        public void Start()
        {
            m_source = new CancellationTokenSource();
            var token = m_source.Token;

            m_updateTask = Task.Factory.StartNew(() => UpdateStations(token), token);
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
            
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetStringAsync(@"http://www.goteborgbikes.se/index.php/service/carto").Result;

            }

            m_updateTask =
                Task
                    .Delay(5*60*1000, cancellationToken)
                    .ContinueWith(t => UpdateStations(cancellationToken), cancellationToken)
                    ;
        }

    }
}
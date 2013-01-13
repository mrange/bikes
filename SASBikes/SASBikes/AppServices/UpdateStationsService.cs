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
                var result = httpClient.GetStringAsync(@"http://www.goteborgbikes.se//index.php/service/carto").Result;

            }

            m_updateTask =
                Task
                    .Delay(5*60*1000, cancellationToken)
                    .ContinueWith(t => UpdateStations(cancellationToken), cancellationToken)
                    ;
        }

    }
}
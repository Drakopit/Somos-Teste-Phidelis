using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Somos_Teste_Phidelis.Domain.Config;
using Somos_Teste_Phidelis.Handler;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Somos_Teste_Phidelis.Api.HostedService
{
    public class TimerHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly TimeSet _timeSet;
        public TimerHostedService(ILogger<TimerHostedService> logger, IOptions<TimeSet> timeSet)
        {
            _logger = logger;
            _timeSet = timeSet.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            new Timer(RefreshStudies, null, TimeSpan.Zero, TimeSpan.FromSeconds(Convert.ToInt32(_timeSet.Time)));
            return Task.CompletedTask;
        }

        private async void RefreshStudies(object? state)
        {
            try
            {
                _logger.LogInformation($"Refreshing {DateTime.UtcNow}");
                await UpdateTableTimer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateTableTimer()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("Https://api/Settings/");
                var result = await client.GetAsync("Update");
                if (result != null)
                    if (result.StatusCode == HttpStatusCode.OK)
                        return true;
                return false;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Finishing Process {DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Somos_Teste_Phidelis.Domain.Config;
using Somos_Teste_Phidelis.Repository.Interfaces;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Somos_Teste_Phidelis.Handler
{
    public class TimerRefreshHandler : ITimerRefreshHandler
    {
        private readonly TimeSet _timeSet;
        private readonly IAlunoRepository _repository;
        
        public TimerRefreshHandler(IOptions<TimeSet> timeSet, IAlunoRepository repository)
        {
            _timeSet = timeSet.Value;
            _repository = repository;
        }

        public async Task UpdateSettingsTimer(int milliseconds)
        {

            _timeSet.Time = milliseconds.ToString();
        }

        public async Task UpdateTableTimer()
        {
            try
            {
                var names = JsonConvert.SerializeObject(await GetNames());

                for (int i = 0; i < names.Length; i++)
                {
                    await _repository.Insert(new Domain.Aluno
                    {
                        Name = names[i].ToString(),
                        Date = DateTime.Now
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GetNames()
        {
            try
            {
                var names = "";
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://gerador-nomes.herokuapp.com");
                    names = await client.GetStringAsync("/nomes/5");
                }
                return names;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

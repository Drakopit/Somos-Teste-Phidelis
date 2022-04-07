using Microsoft.AspNetCore.Mvc;
using Somos_Teste_Phidelis.Handler;
using System;
using System.Threading.Tasks;

namespace Somos_Teste_Phidelis.Api.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ITimerRefreshHandler _timerRefreshHandler;

        public SettingsController(ITimerRefreshHandler timerRefreshHandler)
        {
            _timerRefreshHandler = timerRefreshHandler;
        }

        [HttpPost("{time}")]
        public async Task<IActionResult> UpdateTime(int time)
        {
            try
            {
                await _timerRefreshHandler.UpdateSettingsTimer(time);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateTable()
        {
            try
            {
                await _timerRefreshHandler.UpdateTableTimer();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

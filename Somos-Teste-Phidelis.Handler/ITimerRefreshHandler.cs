using System.Threading.Tasks;

namespace Somos_Teste_Phidelis.Handler
{
    public interface ITimerRefreshHandler
    {
        Task UpdateSettingsTimer(int milliseconds);
        Task UpdateTableTimer();
    }
}

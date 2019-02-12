using Common.Enums;
using Models;

namespace Interfaces
{
    public interface IEngine
    {
        GameSettingsModel GetGameSettings(string sequence);
    }
}

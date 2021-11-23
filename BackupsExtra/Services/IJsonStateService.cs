using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public interface IJsonStateService : IStateService
    {
        void SetJsonSettings(JsonSerializerSettings settings);
    }
}
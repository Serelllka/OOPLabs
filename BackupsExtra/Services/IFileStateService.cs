namespace BackupsExtra.Services
{
    public interface IFileStateService : IStateService
    {
        void SetFilename(string filename);
    }
}
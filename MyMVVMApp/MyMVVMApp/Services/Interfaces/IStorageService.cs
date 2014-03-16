namespace MyMVVMApp.Services.Interfaces
{
    public interface IStorageService
    {
        T Load<T>(string filePath);
        void Save<T>(string filePath, T data);
        bool FileExists(string filePath);
    }
}

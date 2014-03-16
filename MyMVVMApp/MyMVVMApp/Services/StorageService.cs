using MyMVVMApp.Services.Interfaces;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Json;

namespace MyMVVMApp.Services
{
    // Manejo del almacenamiento
    public class StorageService : IStorageService
    {
        // Constructor
        public StorageService()
        {
        }

        // Deserializa un objeto de un fichero en una carpeta
        public T Load<T>(string filePath)
        {
            // Accede al Isolated Storage
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!myIsolatedStorage.FileExists(filePath))
                {
                    // El fichero no existe.
                    return default(T);
                }

                // El fichero existe. Ábrelo
                using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(filePath, FileMode.Open))
                {
                    // Deserializa el contenido
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                    T data = (T)json.ReadObject(stream);
                    return data;
                }
            }
        }

        // Serializa un objeto a fichero en una carpeta
        public void Save<T>(string filePath, T data)
        {
            // Accede al Isolated Storage
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Crea la carpeta
                myIsolatedStorage.CreateDirectory(Path.GetDirectoryName(filePath));

                // Crea el fichero
                using (IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(filePath))
                {
                    // Serializa el objeto al fichero
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                    json.WriteObject(fileStream, data);
                }
            }
        }

        // Comprueba si un fichero existe en una carpeta
        public bool FileExists(string filePath)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                return myIsolatedStorage.FileExists(filePath);
            }
        }
    }
}

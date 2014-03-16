using MyMVVMApp.Resources;
using MyMVVMApp.Services.Interfaces;
using MyMVVMApp.Views.Localization;
using System.IO.IsolatedStorage;

namespace MyMVVMApp.Services
{
    // Configuración de la app
    public class ConfigurationService : IConfigurationService
    {
        // Constructor
        public ConfigurationService()
        {
        }

        // Nombre y ruta del fichero de backup
        public string BackupFilePath { get { return AppResources.App_BackupFilePath; } }

        // Cadenas de texto localizadas que usarán los vista-modelo. 
        public string ItemTitleEdit { get { return AppResources.Item_TitleEdit; } }
        public string ItemTitleNew { get { return AppResources.Item_TitleNew; } }
        public string MessageAddSuccess { get { return AppResources.Message_AddSuccess; } }
        public string MessageEditSuccess { get { return AppResources.Message_EditSuccess; } }
        public string MessageRemoveSuccess { get { return AppResources.Message_RemoveSuccess; } }
        public string MessageLoadBackupError { get { return AppResources.Message_LoadBackupError; } }
        public string MessageSaveBackupError { get { return AppResources.Message_SaveBackupError; } }

        // Alguna propiedad cuyo estado queramos mantener entre ejecuciones de la app
        public string SomeOtherProperty
        {
            get
            {
                string someOtherProperty;
                IsolatedStorageSettings.ApplicationSettings.TryGetValue<string>("SomeOtherProperty", out someOtherProperty);
                return someOtherProperty;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["SomeOtherProperty"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}

namespace MyMVVMApp.Services.Interfaces
{
    public interface IConfigurationService
    {
        string BackupFilePath { get; }
        
        string ItemTitleEdit { get; }
        string ItemTitleNew { get; }

        string MessageAddSuccess { get; }
        string MessageEditSuccess { get; }
        string MessageRemoveSuccess { get; }
        string MessageLoadBackupError { get; }
        string MessageSaveBackupError { get; }

        string SomeOtherProperty { get; set; }
    }
}

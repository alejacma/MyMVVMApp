using MyMVVMApp.ViewModels.Interfaces;

namespace MyMVVMApp.Services.Interfaces
{
    public interface IViewModelLocatorService
    {
        IMainViewModel MainViewModel { get; }
        IItemViewModel ItemViewModel { get; }
        IAboutInfoViewModel AboutInfoViewModel { get; }
    }
}

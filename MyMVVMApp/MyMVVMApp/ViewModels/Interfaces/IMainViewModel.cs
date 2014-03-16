using System.Collections.ObjectModel;
using System.Windows.Input;
using MyMVVMApp.Models;

namespace MyMVVMApp.ViewModels.Interfaces
{
    public interface IMainViewModel
    {
        ObservableCollection<Item> Items { get; }
 
        ICommand CreateItemCommand { get; }
        ICommand EditItemCommand { get; }
        ICommand ShowAboutInfoCommand { get; }
    }
}

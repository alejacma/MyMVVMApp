using System.Collections.ObjectModel;
using System.Windows.Input;
using MyMVVMApp.Models;

namespace MyMVVMApp.ViewModels.Interfaces
{
    public interface IItemViewModel
    {
        string PageTitle { get; }
        string ItemName { get; set; }
        string ItemDescription { get; set; }
 
        ICommand AcceptCommand { get; }
        ICommand CancelCommand { get; }
        ICommand RemoveCommand { get; }
    }
}

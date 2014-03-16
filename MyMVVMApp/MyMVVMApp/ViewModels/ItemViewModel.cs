using MyMVVMApp.Models;
using MyMVVMApp.Services.Interfaces;
using MyMVVMApp.ViewModels.Base;
using MyMVVMApp.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyMVVMApp.ViewModels
{
    // Vista-modelo correspondiente a la página de un elemento
    public class ItemViewModel: BindableBase, IItemViewModel, INavigable
    {
        // Título de la página
        private string _pageTitle;
        public string PageTitle { get { return this._pageTitle; } private set { this.SetProperty(ref this._pageTitle, value); } }

        // Nombre del elemento
        private string _itemName;
        public string ItemName 
        { 
            get 
            { 
                return this._itemName; 
            } 
            set 
            { 
                this.SetProperty(ref this._itemName, value);

                (this.AcceptCommand as DelegateCommand).RaiseCanExecuteChanged();
            } 
        }

        // Descripción del elemento
        private string _itemDescription;
        public string ItemDescription { get { return this._itemDescription; } set { this.SetProperty(ref this._itemDescription, value); } }

        // Elemento sobre el que estamos trabajando. Si está a null estamos creando uno nuevo, y si no, lo estamos editando
        private Item _currentItem;
        private Item CurrentItem
        {
            get
            {
                return this._currentItem;
            }
            set
            {
                this._currentItem = value;

                (this.RemoveCommand as DelegateCommand).RaiseCanExecuteChanged();
            }
        }

        // Parámetros de navegación recibidos de otro vista-modelo
        public object NavigationContext
        {
            set
            {
                // No hagas nada si los parámetros no son válidos
                if (value != null && !(value is Item)) { return; }

                // Info a mostrar en la vista
                Item currentItem = value as Item;
                if (currentItem != null)
                {
                    // Estamos editando un elemento existente
                    this.PageTitle = this.ConfigurationService.ItemTitleEdit;
                    this.ItemName = currentItem.Name;
                    this.ItemDescription = currentItem.Description;
                    this.CurrentItem = currentItem;
                }
            }
        }

        // Comandos
        public ICommand AcceptCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }

        // Servicios dependientes de la plataforma que necesitamos en este vista-modelo
        private IConfigurationService ConfigurationService;
        private INavigationService NavigationService;
        private IMessagingService MessagingService;

        // Constructor
        public ItemViewModel(
            IConfigurationService configurationService,
            INavigationService navigationService, 
            IMessagingService messagingService)
        {
            // Servicios
            this.ConfigurationService = configurationService;
            this.NavigationService = navigationService;
            this.MessagingService = messagingService;

            // Comandos
            this.AcceptCommand = new DelegateCommand(this.Accept, this.CanAccept);
            this.CancelCommand = new DelegateCommand(this.Cancel);
            this.RemoveCommand = new DelegateCommand(this.Remove, this.CanRemove);

            // Info a mostrar en la vista
            this.PageTitle = this.ConfigurationService.ItemTitleNew;
        }

        // El usuario ha aceptado los cambios
        private void Accept()
        {
            if (this.CurrentItem == null)
            {
                // Crea un nuevo elemento
                Item newItem = new Item();
                newItem.Name = this.ItemName.Trim();
                newItem.Description = (this.ItemDescription != null)? this.ItemDescription.Trim() : null;

                // Ve de vuelta a la página principal y dile qué elemento hemos creado y queremos que añada a la colección
                Dictionary<string, object> navigationContext = new Dictionary<string, object>();
                navigationContext.Add("action", "add");
                navigationContext.Add("item", newItem);
                this.NavigationService.NavigateBack(navigationContext);
            }
            else
            {
                // Edita el elemento existente
                this.CurrentItem.Name = this.ItemName.Trim();
                this.CurrentItem.Description = (this.ItemDescription != null) ? this.ItemDescription.Trim() : null;

                // Ve de vuelta a la página principal y dile que ya hemos editado el elemento
                Dictionary<string, object> navigationContext = new Dictionary<string, object>();
                navigationContext.Add("action", "edit");
                navigationContext.Add("item", this.CurrentItem);
                this.NavigationService.NavigateBack(navigationContext);
            }
        }
        // Puede el usuario aceptarlos?
        private bool CanAccept()
        {
            return !string.IsNullOrWhiteSpace(this.ItemName);
        }

        // El usuario ha cancelado los cambios
        private void Cancel()
        {
            // Vuelve a la página anterior
            this.NavigationService.NavigateBack();
        }

        // El usuario quiere borrar este elemento
        private void Remove()
        {
            // Ve de vuelta a la página principal y dile qué elemento queremos que borre de la colección
            Dictionary<string, object> navigationContext = new Dictionary<string, object>();
            navigationContext.Add("action", "remove");
            navigationContext.Add("item", this.CurrentItem);
            this.NavigationService.NavigateBack(navigationContext);
        }
        // Puede el usuario borrarlo?
        private bool CanRemove()
        {
            return (this.CurrentItem != null);
        }
    }
}

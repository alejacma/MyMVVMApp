using MyMVVMApp.Models;
using MyMVVMApp.Services.Interfaces;
using MyMVVMApp.Services.Messaging;
using MyMVVMApp.ViewModels.Base;
using MyMVVMApp.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyMVVMApp.ViewModels
{
    // Vista-modelo correspondiente a la página principal
    public class MainViewModel: BindableBase, IMainViewModel, INavigable
    {
        // Colección de elementos
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items { get { return this._items; } private set { this.SetProperty(ref this._items, value); } }

        // Parámetros de navegación recibidos de otro vista-modelo
        public object NavigationContext
        {
            set
            {
                // No hagas nada si los parámetros no son válidos
                if (value == null) { return; }
                if (!(value is Dictionary<string, object>)) { return; }

                // Mira a ver qué te ha llegado del otro vista-modelo
                Dictionary<string, object> parameters = (value as Dictionary<string, object>);
                object action;
                if (parameters.TryGetValue("action", out action))
                {
                    if ((action as string) == "add")
                    {
                        // Hay que añadir un elemento
                        object item;
                        if (parameters.TryGetValue("item", out item))
                        {
                            this.AddItem(item as Item);
                        }
                    }
                    else if ((action as string) == "edit")
                    {
                        // Se ha editado un elemento
                        object item;
                        if (parameters.TryGetValue("item", out item))
                        {
                            // Guarda los cambios a disco
                            bool success = this.BackupChanges();
                            if (success) 
                            {
                                // Informa al usuario
                                this.MessagingService.Send(typeof(MessageToUser), new MessageToUser(this.ConfigurationService.MessageEditSuccess, 2000));
                            }
                        }
                    }
                    else if ((action as string) == "remove")
                    {
                        // Hay que quitar un elemento
                        object item;
                        if (parameters.TryGetValue("item", out item))
                        {
                            this.RemoveItem(item as Item);
                        }
                    }
                }
            }
        }

        // Comandos
        public ICommand CreateItemCommand { get; private set; }
        public ICommand EditItemCommand { get; private set; }
        public ICommand ShowAboutInfoCommand { get; private set; }

        // Servicios dependientes de la plataforma que necesitamos en este vista-modelo
        private IConfigurationService ConfigurationService;
        private INavigationService NavigationService;
        private IMessagingService MessagingService;
        private IStorageService StorageService;

        // Constructor
        public MainViewModel(
            IConfigurationService configurationService, 
            INavigationService navigationService, 
            IMessagingService messagingService,
            IStorageService storageService)
        {
            // Servicios
            this.ConfigurationService = configurationService;
            this.NavigationService = navigationService;
            this.MessagingService = messagingService;
            this.StorageService = storageService;

            // Comandos
            this.CreateItemCommand = new DelegateCommand(this.CreateItem);
            this.EditItemCommand = new DelegateCommand<Item>(this.EditItem);
            this.ShowAboutInfoCommand = new DelegateCommand(this.ShowAboutInfo);

            // Carga los datos que ya tengamos en disco
            try
            {
                this.Items = this.StorageService.Load<ObservableCollection<Item>>(this.ConfigurationService.BackupFilePath) ?? new ObservableCollection<Item>();
            }
            catch (Exception)
            {
                this.Items = new ObservableCollection<Item>();

                // Informa al usuario de posibles errores
                this.MessagingService.Send(typeof(MessageToUser), new MessageToUser(this.ConfigurationService.MessageLoadBackupError, 3000));
            }
        }

        // Guarda los cambios a disco
        private bool BackupChanges()
        {
            try
            {
                this.StorageService.Save<ObservableCollection<Item>>(
                    this.ConfigurationService.BackupFilePath,
                    this.Items
                );
                return true;
            }
            catch (Exception)
            {
                // Informa al usuario de posibles errores
                this.MessagingService.Send(typeof(MessageToUser), new MessageToUser(this.ConfigurationService.MessageSaveBackupError, 3000));
                return false;
            }
        }

        // El usuario ha añadido un elemento a la colección
        private void AddItem(Item item)
        {
            this.Items.Add(item);

            // Guarda los cambios a disco
            bool success = this.BackupChanges();
            if (success)
            {
                // Informa al usuario
                this.MessagingService.Send(typeof(MessageToUser), new MessageToUser(this.ConfigurationService.MessageAddSuccess, 2000));
            }            
        }

        // El usuario ha quitado un elemento de la colección
        private void RemoveItem(Item item)
        {
            this.Items.Remove(item);

            // Guarda los cambios a disco
            bool success = this.BackupChanges();
            if (success)
            {
                // Informa al usuario
                this.MessagingService.Send(typeof(MessageToUser), new MessageToUser(this.ConfigurationService.MessageRemoveSuccess, 2000));
            }
        }

        // El usuario quiere crear un nuevo elemento
        private void CreateItem()
        {
            this.NavigationService.NavigateTo<IItemViewModel>();
        }

        // El usuario quiere editar un elemento
        private void EditItem(Item item)
        {
            this.NavigationService.NavigateTo<IItemViewModel>(item);
        }

        // El usuario quiere ver el Acerca De de la app
        private void ShowAboutInfo()
        {
            this.NavigationService.NavigateTo<IAboutInfoViewModel>();
        }
    }
}

using Microsoft.Practices.Unity;
using MyMVVMApp.Services.Interfaces;
using MyMVVMApp.ViewModels;
using MyMVVMApp.ViewModels.Interfaces;

namespace MyMVVMApp.Services
{    
    // Provee a una vista del vista-modelo que necesite
    //
    // Nota: La clase UnityContainer está en esta librería:
    //
    // Microsoft Unity 2.1 for Silverlight
    // http://www.microsoft.com/en-us/download/details.aspx?id=1623
    //
    public class ViewModelLocatorService: IViewModelLocatorService
    {
        // Contenedor para Inversión de Control (IoC - Inversion of Control) por injección de dependencias (DI - Dependency Injection)
        private IUnityContainer _container;

        // Constructor
        public ViewModelLocatorService()
        {
            // Crea el contenedor IoC
            this._container = new UnityContainer();

            // Añade los servicios de los que dependen los vista-modelo
            this._container.RegisterType<IConfigurationService, ConfigurationService>();
            this._container.RegisterType<INavigationService, NavigationService>();
            this._container.RegisterType<IMessagingService, MessagingService>();
            this._container.RegisterType<IStorageService, StorageService>();
            
            // Añade los vista-modelo y asociales los servicios que les corresponda (especificados en el constructor de cada vista-modelo)
            this._container.RegisterType<IMainViewModel, MainViewModel>(new ContainerControlledLifetimeManager()); // Una sola copia
            this._container.RegisterType<IItemViewModel, ItemViewModel>();
            this._container.RegisterType<IAboutInfoViewModel, AboutInfoViewModel>();
        }

        // Vista-modelo de la página principal
        public IMainViewModel MainViewModel { get { return this._container.Resolve<MainViewModel>(); } }

        // Vista-modelo de la página de un elemento
        public IItemViewModel ItemViewModel { get { return this._container.Resolve<ItemViewModel>(); } }

        // Vista-modelo de la página de configuración
        public IAboutInfoViewModel AboutInfoViewModel { get { return this._container.Resolve<AboutInfoViewModel>(); } }

    }
}

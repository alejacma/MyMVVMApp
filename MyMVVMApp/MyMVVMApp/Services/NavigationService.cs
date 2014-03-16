using Microsoft.Phone.Controls;
using MyMVVMApp.Services.Interfaces;
using MyMVVMApp.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;

namespace MyMVVMApp.Services
{
    // Navegación entre páginas
    public class NavigationService : INavigationService
    {
        // Correspondencia entre los vista-modelo y sus vistas
        private static IDictionary<Type, string> _viewModelRouting = new Dictionary<Type, string>() 
        { 
            { typeof(IMainViewModel), "/Views/MainPage.xaml" },
            { typeof(IItemViewModel), "/Views/ItemPage.xaml" },
            { typeof(IAboutInfoViewModel), "/Views/AboutInfoPage.xaml" }
        };     

        // Parámetros complejos que se pasan a los vista-modelo asociados a las vistas
        private object _navigationContext;

        // Navega a la vista de un vista-modelo sin paso de parámetros
        public void NavigateTo<TDestinationViewModel>()
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigate(new Uri(_viewModelRouting[typeof(TDestinationViewModel)], UriKind.Relative));
        }

        // Navega a una vista pasándole parámetros complejos a su vista-modelo
        public void NavigateTo<TDestinationViewModel>(object navigationContext)
        {
            // Parámetros para el vista-modelo
            this._navigationContext = navigationContext;

            // Navega a la página y entérate de cuándo hemos llegado
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigated += new NavigatedEventHandler(Page_Navigated);
            rootFrame.Navigate(new Uri(_viewModelRouting[typeof(TDestinationViewModel)], UriKind.Relative));
        }

        // Navega de vuelta a la vista anterior, sin pasarle parámetros a su vista-modelo
        public void NavigateBack()
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }
        
        // Navega de vuelta a la vista anterior, pasándole parámetros complejos a su vista-modelo
        public void NavigateBack(object navigationContext)
        {
            // Parámetros para el vista-modelo
            this._navigationContext = navigationContext;

            // Navega a la página anterior y entérate de cuándo hemos llegado
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.Navigated += new NavigatedEventHandler(Page_Navigated);
                rootFrame.GoBack();
            }
        }

        // Ya hemos llegado a la página a cuyo vista-modelo queremos pasarle los parámetros
        private void Page_Navigated(object sender, NavigationEventArgs e)
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigated -= Page_Navigated;

            // Pásale los parámetros a su vista-modelo
            ((e.Content as PhoneApplicationPage).DataContext as INavigable).NavigationContext = this._navigationContext;
        }

        // Limpia el historial de navegación entre páginas
        public void ClearNavigationHistory()
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            while (rootFrame.RemoveBackEntry() != null) ;
        }
    }
}
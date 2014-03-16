MyMVVMApp
=========

Ejemplo de implementación del patrón MVVM en Windows Phone 8
http://alejandrocamposmagencio.com/2013/04/22/windows-phone-tips-tricks-ejemplo-de-implementacion-del-patron-mvvm/

Entre otras cosas, podrás encontrar en este ejemplo lo siguiente:
1.Estructura básica de MVVM: vista, vista-modelo y modelo (carpetas Views, ViewModels y Models).
2.Uso de servicios para las partes dependientes de la plataforma que no podemos poner en los vista-modelo y modelo (carpeta Services).
3.Localizador de vista-modelo para las vistas (clase ViewModelLocatorService). Incluye el paso de servicios a los vista-modelo mediante inversión de control (IoC – Inversion of Control) por inyección de dependencias (DI – Dependency Injection) usando Unity.
4.Navegación entre vistas controlada por los vista-modelo (clase NavigationService), con paso de parámetros complejos entre los vista-modelo de dichas vistas.
5.Envío de mensajes de los vista-modelo a las vistas usando MVVM Light (clase MessagingService).
6.Implementación de INotifyPropertyChanged para el binding de datos entre las vistas y sus vista-modelo (clase BindableBase).
7.Implementación de ICommand para el binding de comandos entre las vistas y sus vista-modelo (clase DelegateCommand), incluyendo el uso de CanExecute en los comandos.
8.Binding de comandos a la ApplicationBar de las vistas directamente en XAML con AppBarUtils.


Alejandro Campos Magencio
Microsoft Technical Evangelist


using GalaSoft.MvvmLight.Messaging;
using System;
using MyMVVMApp.Services.Interfaces;

namespace MyMVVMApp.Services
{
    // Envío de mensajes entre diferentes partes de la app
    //
    // Nota: La clase Messenger es parte de MVVM Light, cuyo paquete NuGet v4.1.27.0 he referenciado en el proyecto.
    //
    public class MessagingService: IMessagingService
    {
        // Constructor
        public MessagingService()
        {
        }

        // Envía un mensaje vacío
        public void Send(object token)
        {
            Messenger.Default.Send<object>(null, token);
        }

        // Envía un mensaje
        public void Send(object token, object message)
        {
            Messenger.Default.Send<object>(message, token);
        }

        // Registrate para recibir un mensaje
        public void Register(object recipient, object token, Action<object> action)
        {
            Messenger.Default.Register<object>(recipient, token, action);
        }
    }
}

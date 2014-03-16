using Microsoft.Phone.Controls;
using MyMVVMApp.Models;
using MyMVVMApp.Services;
using MyMVVMApp.Services.Messaging;
using System;
using System.Windows.Threading;

namespace MyMVVMApp.Views
{
    // Página principal
    public partial class MainPage : PhoneApplicationPage
    {
        // Temporizador para los mensajes que se muestran al usuario
        private DispatcherTimer _timer;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Registrate para recibir mensajes de los vista-modelo
            new MessagingService().Register(this, typeof(MessageToUser), new Action<object>(this.ShowMessageToUser));

            // Prepara el temporizador para borrar el mensaje al usuario tras el tiempo indicado
            this.MessageToUser.Text = null;
            this._timer = new DispatcherTimer();
            this._timer.Tick += new EventHandler((s, e) => { this.MessageToUser.Text = null; this._timer.Stop(); });
        }

        // Muestra un mensaje al usuario de parte de un vista-modelo
        private void ShowMessageToUser(object messageObject)
        {
            // Si no hay ningún mensaje no hagas nada
            MessageToUser messageToUser = messageObject as MessageToUser;
            if (messageToUser == null)
            {
                return;
            }

            // Muestra el mensaje durante el tiempo indicado
            this.MessageToUser.Text = messageToUser.Text;
            if (messageToUser.Milliseconds >= 0)
            {
                // Muéstralo durante x milisegundos
                this._timer.Interval = TimeSpan.FromMilliseconds(messageToUser.Milliseconds);
                this._timer.Start();
            }
            else
            {
                // Muéstralo por tiempo indeterminado
                this._timer.Stop();
            }
        }
    }
}
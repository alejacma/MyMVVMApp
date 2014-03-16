namespace MyMVVMApp.Services.Messaging
{
    // Mensajes para mostrar al usuario
    public class MessageToUser
    {
        // Mensaje
        private string _text;
        public string Text { get { return this._text; } set { this._text = value; } }

        // Duración
        private int _milliseconds;
        public int Milliseconds { get { return this._milliseconds; } set { this._milliseconds = value; } }

        // Constructor
        public MessageToUser(string message)
            : this(message, -1)
        {
        }
        public MessageToUser(string message, int milliseconds)
        {
            this.Text = message;
            this.Milliseconds = milliseconds;
        }
    }
}

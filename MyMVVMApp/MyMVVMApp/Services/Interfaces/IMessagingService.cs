using System;

namespace MyMVVMApp.Services.Interfaces
{
    public interface IMessagingService
    {
        void Send(object token);
        void Send(object token, object message);
        void Register(object recipient, object token, Action<object> action);
    }
}

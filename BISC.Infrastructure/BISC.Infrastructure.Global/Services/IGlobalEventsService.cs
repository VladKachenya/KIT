using System;

namespace BISC.Infrastructure.Global.Services
{
    public interface IGlobalEventsService
    {
        void SendMessage<T>(T message);
        void Subscribe<T>(Action<T> action);
        void Unsubscribe<T>(Action<T> action);
    }
}
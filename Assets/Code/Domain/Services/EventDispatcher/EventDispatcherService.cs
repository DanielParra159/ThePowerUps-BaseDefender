namespace Domain.Services.EventDispatcher
{
    public delegate void SignalDelegate(ISignal signal);

    public interface EventDispatcherService
    {
        void Subscribe<T>(SignalDelegate callback) where T : ISignal;
        void Unsubscribe<T>(SignalDelegate callback) where T : ISignal;
        void Dispatch<T>(T signal) where T : ISignal;
    }
}
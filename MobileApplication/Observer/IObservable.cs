namespace Observer
{
    public interface IObservable<T>
    {
        void Subscribe(params T[] observer);
        void Unsubscribe(params T[] observer);
        void Notify();
    }
}

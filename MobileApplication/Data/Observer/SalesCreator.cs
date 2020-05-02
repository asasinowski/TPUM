using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Observer
{
    public class SalesCreator : IObservable<PizzaEvent>
    {
        private IList<IObserver<PizzaEvent>> observers;

        public SalesCreator()
        {
            observers = new List<IObserver<PizzaEvent>>();
        }

        public IDisposable Subscribe(IObserver<PizzaEvent> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        public class Unsubscriber : IDisposable
        {
            private IList<IObserver<PizzaEvent>> _observers;
            private IObserver<PizzaEvent> _observer;

            public Unsubscriber(IList<IObserver<PizzaEvent>> observers, IObserver<PizzaEvent> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                Dispose(true);
            }
            private bool _disposed = false;
            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }
                if (disposing)
                {
                    if (_observer != null && _observers.Contains(_observer))
                    {
                        _observers.Remove(_observer);
                    }
                }
                _disposed = true;
            }
        }
        public void Sale(PizzaEvent pizza)
        {
            foreach (var observer in observers)
            {
                if (pizza == null)
                {
                    observer.OnError(new ArgumentNullException());
                }
                observer.OnNext(pizza);
            }
        }
        public void End()
        {
            foreach (var observer in observers.ToArray())
            {
                observer.OnCompleted();
            }
            observers.Clear();
        }
    }
}

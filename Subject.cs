using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementProject
{
    public class Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        // Add observer to the list
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        // Remove observer from the list
        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        // Notify all observers
        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
    }

}

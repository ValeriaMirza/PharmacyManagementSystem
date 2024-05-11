using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementProject
{
    public class MedicineOriginator
    {
        public string MedID { get; set; }
        public long Quantity { get; set; }

        public MedicineMemento Save()
        {
            return new MedicineMemento(MedID, Quantity);
        }

        public void Restore(MedicineMemento memento)
        {
            MedID = memento.MedID;
            Quantity = memento.Quantity;
        }

    }
}

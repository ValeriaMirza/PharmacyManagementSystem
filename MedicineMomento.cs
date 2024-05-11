using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementProject
{
    public class MedicineMemento
    {
        public string MedID { get; }
        public long Quantity { get; }

        public MedicineMemento(string medID, long quantity)
        {
            MedID = medID;
            Quantity = quantity;
        }
    }
}

using PharmacyManagementProject.Pharmacist_UC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementProject
{
    interface IMedicineState
    {
        void HandleState(UC_P_MedicineValidityCheck context);
    }
}

using PharmacyManagementProject.Pharmacist_UC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementProject
{
    class ValidState : IMedicineState
    {
        public void HandleState(UC_P_MedicineValidityCheck context)
        {
            string query = "select * from medicine where eDate >= getdate()";
            context.SetDataGridView(query, "Valid medicines.", Color.Green);
        }
    }
}

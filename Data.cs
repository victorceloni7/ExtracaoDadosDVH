using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace RelatorioTratamentoDVH
{
    public class Data
    {
        public string RecordNumber;
        public string StartTime;
        public string EndTime;
        public string UserName;
        public string PatientId;
        public string PlanSetupId;
        public string PlansInScopeId;
        public string PlanSumsInScopeId;
        public Data(ScriptContext context)
        {
            UserName = (context.CurrentUser != null) ? context.CurrentUser.ToString() : "";
            PatientId = (context.Patient != null) ? context.Patient.ToString() : "";
            PlanSetupId = (context.PlanSetup != null) ? string.Format("[{0}:{1}]", context.PlanSetup.Course.ToString(), context.PlanSetup.ToString()) : "";
            PlansInScopeId = string.Join("; ", context.PlansInScope.Select(x => string.Format("[{0}:{1}]", x.Course.ToString(), x.ToString()))) ?? "Unknown";
            PlanSumsInScopeId = string.Join("; ", context.PlanSumsInScope.Select(x => string.Format("[{0}:{1}]", x.Course.ToString(), x.ToString()))) ?? "Unknown";
           
        }
    }
}

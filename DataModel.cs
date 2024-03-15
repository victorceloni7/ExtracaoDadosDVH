using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace RelatorioTratamentoDVH
{
    public class DataModel
    {
        // Class properties
        public List<StructureObjective> Objectives; // List of DVH Objectives that will be checked. //all the dose values are internally in Gy.
        public List<string> UniqueStructures; // List of all structures in the objectives
        public List<PlanResult> Results = new List<PlanResult>(); // List that contains results for each plan.
        public Dictionary<string, string> WarningDictionary = new Dictionary<string, string>(); // Dictionary for warning numbers and definitions
        public Dictionary<StructureSet, Dictionary<string, Structure>> StructureDictionary = new Dictionary<StructureSet, Dictionary<string, Structure>>(); // Dictionary to map structure names and structure for each structure set
        public string Mode;

        // Parameters currently loaded in Context
        public User ContextUser { get; set; }
        public Patient ContextPatient { get; set; }
        public Image ContextImage { get; set; }
        public StructureSet ContextStructureSet { get; set; }
        public PlanSetup ContextPlanSetup { get; set; }
        public IEnumerable<PlanSetup> ContextPlanSetupsInScope { get; set; }
        public IEnumerable<PlanSum> ContextPlanSumsInScope { get; set; }

        // Constructors
        public DataModel(ScriptContext context, string mode) : this(context.CurrentUser, context.Patient, context.Image, context.StructureSet, context.PlanSetup, context.PlansInScope, context.PlanSumsInScope, mode) { }
        public DataModel(User user, Patient patient, Image image, StructureSet structureSet, PlanSetup planSetup, IEnumerable<PlanSetup> planSetupsInScope, IEnumerable<PlanSum> planSumsInScope, string mode)
        {
            ContextUser = user;
            ContextPatient = patient;
            ContextImage = image;
            ContextStructureSet = structureSet;
            ContextPlanSetup = planSetup;
            ContextPlanSetupsInScope = planSetupsInScope;
            ContextPlanSumsInScope = planSumsInScope;
            Mode = mode;

        }
    }
}

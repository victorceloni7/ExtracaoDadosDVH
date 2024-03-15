﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace RelatorioTratamentoDVH
{
    public class PlanResult
    {
        // Properties
        public Patient Patient { get; set; }
        public PlanningItem PlanningItem { get; set; }
        public StructureSet StructureSet { get; set; }
        public Dictionary<string, Structure> StructureSetDict { get; set; }
        public string CourseName { get; set; }
        public string PlanName { get; set; }
        public string PlanType { get; set; }
        public List<StructureObjectiveResult> StructureObjectiveResults = new List<StructureObjectiveResult>();

        // Create and evaluate plan-specific StructureObjectiveResults list
        public void ComputeResults(List<StructureObjective> objectives)
        {
            foreach (StructureObjective objective in objectives)
            {
                StructureObjectiveResult planObjective = new StructureObjectiveResult { Objective = objective, Plan = PlanningItem };
                planObjective.EvaluateObjective(StructureSetDict);
                StructureObjectiveResults.Add(planObjective);
            }
        }
    }
    
  
}

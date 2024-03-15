using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using RelatorioTratamentoDVH.Views;
using RelatorioTratamentoDVH.ViewModels;
using Microsoft.Win32;
using RelatorioTratamentoDVH;
using System.Data;
using System.IO;
using iTextSharp.text.log;
using System.Windows.Interop;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Asn1.X509;



// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
// [assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{

    public class Script
    {

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext context, System.Windows.Window window/*, ScriptEnvironment environment*/)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            // MVVM
            // - Model
            // - View
            // - Viewmodel

            // new instance of our main view - user control
            MainView mainView = new MainView { DataContext = new MainViewModel() };

            // set the eclipse script's window content to show our view
            window.Content = mainView;

            //webinars & workshops/Developer Workshop 2016/katas/intermediate.1


            Data data = new Data(context);

            PlanSetup plan = context.PlanSetup;


            if (plan == null)
            {
                MessageBox.Show("Abra um plano antes de rodar este Script");
                return;
            }
            StructureSet ss = context.StructureSet;
            if (ss == null)
            {
                MessageBox.Show("Abra um conjunto de estruturas antes de rodar este script");
                return;
            }

            //Codigo para gerar dados de uma única estrutura:
            
            //var listStructures = context.StructureSet.Structures;
            //Structure ptv = null;
            
            /*foreach (Structure scan in listStructures)
            {
                if (scan.Id == "PTV1_P1")
                {
                    ptv = scan;
                }

            }*/

            //string msg = string.Format("PTV volume = {0}", ptv.Volume);
            //MessageBox.Show(msg);
            
            //Criação de uma tabela com todos os dados de todas as estruturas presentes no plano.

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Estrutura");
            dataTable.Columns.Add("D Max (cGy)");
            //dataTable.Columns.Add("D Med (cGy)");
            dataTable.Columns.Add("Volume (ccm)");
            dataTable.Columns.Add("D at Vol (95%)"); //pode ser alterado para qualquer valor de dose no volume.
            
            foreach (var structure in plan.StructureSet.Structures)
            {
                // Calculate DVH for each structure
                var dvh = plan.GetDVHCumulativeData(structure, DoseValuePresentation.Absolute, VolumePresentation.Relative, 0.1);
                
                // Add DVH data to the DataTable
                DataRow row = dataTable.NewRow();
                row["Estrutura"] = structure.Id;
                row["D Max (cGy)"] = dvh?.MaxDose.Dose.ToString("F1") ?? "N/A";
                //row["D Med (cGy)"] = dvh?.MeanDose.Dose.ToString("F1") ?? "N/A";
                row["Volume (ccm)"] = dvh?.Volume.ToString("F1") ?? "N/A";
                row["D at Vol (95%)"] = plan.GetDoseAtVolume(structure,95,VolumePresentation.Relative, DoseValuePresentation.Absolute);
                dataTable.Rows.Add(row);
            }
            
            mainView.DataContext = dataTable.DefaultView; 
            




        }

        




    }
        

}


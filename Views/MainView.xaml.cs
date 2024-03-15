using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VMS.TPS.Common.Model.Types;
using VMS.TPS.Common.Model.API;


namespace RelatorioTratamentoDVH.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
     

    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = this;
            Width = 595;
            Height = 842;
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            ofd.FileName = @"C:\Users\Aluno\Downloads\TratamentoProstata.csv";

            ArquivoCSV ArquivoTratamento = new ArquivoCSV();
            string[] TratamentoArray;
            DataTable dt = new DataTable();
            dt.Columns.Add("Estrutura", typeof(string));
            dt.Columns.Add("Vol max (%)", typeof(string));
            dt.Columns.Add("D max (cGy)", typeof(string));
          
            using (StreamReader streamReader = new StreamReader(ofd.FileName))
            {
                while (!streamReader.EndOfStream)
                {
                    TratamentoArray = streamReader.ReadLine().Split(',');

                    ArquivoTratamento.Estrutura = TratamentoArray[0];
                    ArquivoTratamento.Volume_p = TratamentoArray[1];
                    ArquivoTratamento.Dose_a = TratamentoArray[2];
                    
                    dt.Rows.Add(TratamentoArray);
                }
                
                DataView dv = new DataView(dt);
                dtGridView.ItemsSource = dv;
                
            }

        }
        
        private void print_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                this.IsEnabled = false;
                print.Visibility = Visibility.Collapsed;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog().GetValueOrDefault(false))
                {
                    printDialog.PrintVisual(_Report, "Report");
                }

            }
            finally
            {
                this.IsEnabled = true;
                print.Visibility = Visibility.Visible;
            }
        }
    }
}

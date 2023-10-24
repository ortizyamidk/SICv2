using LiveCharts;
using System;
using System.Collections.Generic;
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
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public HomeView()
        {
            InitializeComponent();
            Loaded += HomeView_Loaded;
        }

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            DepartamentoRepository departamentoRepository = new DepartamentoRepository(); // Asegúrate de que la configuración sea la correcta

            // Llama al método GetAreasTerminadas y guarda el resultado en una variable.
            int areast = departamentoRepository.GetAreasTerminadas();

            areasTerminadas.Text = areast.ToString();

            int totalareas = departamentoRepository.GetTotalAreas();

            double div = (double)areast / totalareas;
            double perc = div * 100;
            int percEntero = (int)perc;


            avanceGral.Text = percEntero.ToString() + "%";

            double valoravanceD = div * 10; //2
            int valoravance = (int)valoravanceD;


            int valorfaltante = 10 - valoravance;

            if(valorAvance != null && valorFaltante != null)
            {
                valorAvance.Values = new ChartValues<double> { valoravance };
                valorFaltante.Values = new ChartValues<double> { valorfaltante };
            }
             
        }
    }
}

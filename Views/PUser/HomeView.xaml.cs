using LiveCharts;
using System;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{

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

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            


             
        }
    }
}

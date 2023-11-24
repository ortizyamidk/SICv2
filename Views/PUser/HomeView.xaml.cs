using LiveCharts;
using System;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{

    public partial class HomeView : UserControl
    {
        AreaRepository areaRepository;

        public HomeView()
        {
            InitializeComponent();
            Loaded += HomeView_Loaded;

            areaRepository = new AreaRepository();
        }

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                // Llama al método GetAreasTerminadas y guarda el resultado en una variable.
                int areast = areaRepository.GetAreasTerminadas();

                areasTerminadas.Text = areast.ToString();

                int totalareas = areaRepository.GetTotalAreas();

                if(totalareas == 0)
                {
                    avanceGral.Text = "0%";
                    valorAvance.Values = new ChartValues<double> { 0 };
                    valorFaltante.Values = new ChartValues<double> { 10 };
                }
                else
                {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }                
        }
    }
}

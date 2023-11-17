using LiveCharts;
using System;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views.GUser
{
    public partial class HomeGView : UserControl
    {
        public HomeGView()
        {
            InitializeComponent();
            Loaded += HomeGView_Loaded;
        }

        private void HomeGView_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (HomeGViewModel)DataContext;

            try
            {
                int count = viewModel.CountCursosRegistered; 
                int count2 = viewModel.CountCursos;         

                if (count == 0 && count2 == 0)
                {
                    txtporcentaje.Text = "0%";
                    valor1.Values = new ChartValues<double> { 0 }; 
                    valor2.Values = new ChartValues<double> { 10 };
                }
                else
                {
                    double countD = Convert.ToDouble(count);
                    double count2D = Convert.ToDouble(count2);
                    double division = countD / count2D;

                    int porcentaje = (int)(division * 100);

                    txtporcentaje.Text = porcentaje + "%";

                    int valor1I = (int)(division * 10);
                    int valor2I = 10 - valor1I;

                    if (valor1 != null && valor2 != null)
                    {
                        valor1.Values = new ChartValues<double> { valor1I }; 
                        valor2.Values = new ChartValues<double> { valor2I }; 
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

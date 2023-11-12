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
using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.AspNet.SignalR.Client;

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

            int count = viewModel.CountCursosRegistered; //2
            int count2 = viewModel.CountCursos; //6         

            if (count == 0 && count2 == 0)
            {
                txtporcentaje.Text = "0%";
                valor1.Values = new ChartValues<double> { 0 }; // Establece el valor deseado para valor1
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
                    // Establece los valores para las series
                    valor1.Values = new ChartValues<double> { valor1I }; // Establece el valor deseado para valor1
                    valor2.Values = new ChartValues<double> { valor2I }; // Establece el valor deseado para valor2
                }
            }
        }
    }
}

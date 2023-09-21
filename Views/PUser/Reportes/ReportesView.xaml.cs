using MaterialDesignThemes.Wpf;
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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para ReportesView.xaml
    /// </summary>
    public partial class ReportesView : UserControl
    {
        public ReportesView()
        {
            InitializeComponent();
        }

        private void btnGeneral_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "Generar reporte de la Lista General del Personal Calificado de la Empresa";
        }

        private void btnDC4_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "Generar reporte de la Lista de Constancias de Habilidades Laborales de los Trabajadores de la Empresa";
        }
    }
}

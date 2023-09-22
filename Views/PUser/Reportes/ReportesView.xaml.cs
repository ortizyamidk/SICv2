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
            icono.Icon=FontAwesome.Sharp.IconChar.File;
            txtDescripcion.Text = "Generar reporte de la Lista General del Personal Calificado de la Empresa";
            btnC.Content = "Cancelar";
            btnA.Content = "Aceptar";
        }

        private void btnDC4_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.File;
            txtDescripcion.Text = "Generar reporte de la Lista de Constancias de Habilidades Laborales de los Trabajadores de la Empresa";
            btnC.Content = "Cancelar";
            btnA.Content = "Aceptar";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.Search;
            txtDescripcion.Text = "Generar reporte de la Lista de Personal Calificado seleccionando un Área de Departamento específica";
            btnC.Content = "Cancelar";
            btnA.Content = "Buscar área";
        }

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.Search;
            txtDescripcion.Text = "Generar reporte de los Cursos Tomados que posee cada Trabajador de la Empresa";
            btnC.Content = "Cancelar";
            btnA.Content = "Buscar trabajador";
        }

        private void btnDC3_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.Search;
            txtDescripcion.Text = "Generar reporte de la Constancia de Habilidades Laborales en relación a los cursos de capacitación de cada Trabajador";
            btnC.Content = "Cancelar";
            btnA.Content = "Buscar trabajador";
        }

        private void btnAsistencia_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.Search;
            txtDescripcion.Text = "Generar reporte de la Lista de Asistencia de cada Curso";
            btnC.Content = "Cancelar";
            btnA.Content = "Buscar curso";
        }
    }
}

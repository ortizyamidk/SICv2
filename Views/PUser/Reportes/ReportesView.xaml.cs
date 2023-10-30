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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para ReportesView.xaml
    /// </summary>
    public partial class ReportesView : UserControl
    {
        DepartamentoRepository departamentoRepository;
        

        public ReportesView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            departamentoRepository = new DepartamentoRepository();
        }

        private void CargarAreas()
        {
            var areas = departamentoRepository.GetDepartamentos();
            foreach (var area in areas)
            {
                var item = new ComboBoxItem
                {
                    Content = area.NomDepto
                };
                cbArea.Items.Add(item);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CargarAreas();
        }

        private void btnGeneral_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDC4_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
            string area = areaseleccionada.Content.ToString();
        }

        private void btnHistorial_Click(object sender, RoutedEventArgs e)
        {
            string numficha = buscarTrab.Text;
        }

        private void btnDC3_Click(object sender, RoutedEventArgs e)
        {
            int numficha = int.Parse(buscarDC3.Text);

            TrabajadorRepository trabajadorRepository = new TrabajadorRepository();

            TrabajadorModel trabajador = trabajadorRepository.FormatoDC3(numficha);

            
        }

        private void btnAsistencia_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "Se generará reporte en formato de Excel";

            string numcurso = buscarCurso.Text;
        }

        private void buscarTrab_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado es numérico
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Evita que se ingrese el carácter no numérico
            }
        }

        // Método para verificar si una cadena es numérica
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un entero
        }

        private void TextBox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            if (!IsLetter(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsLetter(string text)
        {
            return text.All(char.IsLetter);
        }
    }
}

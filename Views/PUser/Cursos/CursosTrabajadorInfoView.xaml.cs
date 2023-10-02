using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static WPF_LoginForm.Views.CursosView;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para CursosTrabajadorInfoView.xaml
    /// </summary>
    public partial class CursosTrabajadorInfoView : UserControl
    {
        public CursosTrabajadorInfoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;


            var converter = new BrushConverter();
            ObservableCollection<CursoTrabajador> listaAsistencia = new ObservableCollection<CursoTrabajador>();

            listaAsistencia.Add(new CursoTrabajador { num = "57019", nombre = "Perez Arrendo Manuel Eduardo", puesto = "Acomodo de material" });
            listaAsistencia.Add(new CursoTrabajador { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            listaAsistencia.Add(new CursoTrabajador { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            listaAsistencia.Add(new CursoTrabajador { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            listaAsistencia.Add(new CursoTrabajador { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            listaAsistencia.Add(new CursoTrabajador { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            listaAsistencia.Add(new CursoTrabajador { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            listaAsistencia.Add(new CursoTrabajador { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            listaAsistencia.Add(new CursoTrabajador { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            listaAsistencia.Add(new CursoTrabajador { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            listaAsistencia.Add(new CursoTrabajador { num = "1", nombre = "Trabajador 1", puesto = "Puesto 1" });
            listaAsistencia.Add(new CursoTrabajador { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            listaAsistencia.Add(new CursoTrabajador { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            listaAsistencia.Add(new CursoTrabajador { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            listaAsistencia.Add(new CursoTrabajador { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            listaAsistencia.Add(new CursoTrabajador { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            listaAsistencia.Add(new CursoTrabajador { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            listaAsistencia.Add(new CursoTrabajador { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            listaAsistencia.Add(new CursoTrabajador { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            listaAsistencia.Add(new CursoTrabajador { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            listaAsistencia.Add(new CursoTrabajador { num = "1", nombre = "Trabajador 1", puesto = "Puesto 1" });
            listaAsistencia.Add(new CursoTrabajador { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            listaAsistencia.Add(new CursoTrabajador { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            listaAsistencia.Add(new CursoTrabajador { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            listaAsistencia.Add(new CursoTrabajador { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            listaAsistencia.Add(new CursoTrabajador { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            listaAsistencia.Add(new CursoTrabajador { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            listaAsistencia.Add(new CursoTrabajador { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            listaAsistencia.Add(new CursoTrabajador { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            listaAsistencia.Add(new CursoTrabajador { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            cursosTrabajadorDataGrid.ItemsSource = listaAsistencia;
        }

        public class CursoTrabajador
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(); // Establece el enfoque en el TextBox
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Ingrese un No. de curso", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtSearch.Focus();
            }
            else
            {
                //hacer consulta. Hacer otro if por si no existe el curso
            }            
        }

        private void btnSig_Click(object sender, RoutedEventArgs e)
        {
            txtNoCurso.Text = "129";
            txtCurso.Text = "Medio Ambiente y Seguridad";
            txtArea.Text = "Ambiental";
        }

        private void btnAnt_Click(object sender, RoutedEventArgs e)
        {
            txtNoCurso.Text = "127";
            txtCurso.Text = "Seguridad y Mantenimiento";
            txtArea.Text = "Mantenimiento";
        }
    }
}

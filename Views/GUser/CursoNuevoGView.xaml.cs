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
using static WPF_LoginForm.Views.GUser.CursoGView;

namespace WPF_LoginForm.Views.GUser
{
    /// <summary>
    /// Lógica de interacción para CursoNuevoGView.xaml
    /// </summary>
    public partial class CursoNuevoGView : UserControl
    {
        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;


            var converter = new BrushConverter();
            ObservableCollection<Participante> participantes = new ObservableCollection<Participante>();

            participantes.Add(new Participante { num = "1", nombre = "Marcella Yamilet Ortiz Guillén", puesto = "Analista de Laboratorio Insp. Recibo de Comp" });
            participantes.Add(new Participante { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            participantes.Add(new Participante { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            participantes.Add(new Participante { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            participantes.Add(new Participante { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            participantes.Add(new Participante { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            participantes.Add(new Participante { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            participantes.Add(new Participante { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            participantes.Add(new Participante { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            participantes.Add(new Participante { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            participantesDataGrid.ItemsSource = participantes;

        }

      

        public class Participante
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.ThumbsUp;
            txtDescripcion.Text = "¡Registro guardado correctamente!";
            btnA.Content = "Aceptar";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dtInicia.Focus();
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.Xmark;
            txtDescripcion.Text = "No se ha encontrado el registro";
            btnA.Content = "Aceptar";
        }
    }
}

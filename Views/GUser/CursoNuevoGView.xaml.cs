using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
using WPF_LoginForm.CustomControls;
using static WPF_LoginForm.Views.CursosView;
using static WPF_LoginForm.Views.GUser.CursoGView;

namespace WPF_LoginForm.Views.GUser
{
    /// <summary>
    /// Lógica de interacción para CursoNuevoGView.xaml
    /// </summary>
    public partial class CursoNuevoGView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            // Suscribir al evento SelectionChanged del ComboBox
            cbInstructor.SelectionChanged += ComboBox_SelectionChanged;
            // Suscribir al evento LostFocus del TextBox
            txtcbInstructor.LostFocus += TextBox_LostFocus;

            // Suscribir al evento SelectionChanged del TimePicker
            tiHorario.SelectedTimeChanged += TiHorario_SelectedTimeChanged;
            txtDuracion_TextChanged(txtDuracion, new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
            txtLugar_TextChanged(txtLugar, new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));

            if (string.IsNullOrEmpty(dtInicia.ToString()))
            {
                errIn.Content = req;
                errIn.Visibility = Visibility.Visible;
            }
            else
            {
                errIn.Visibility = Visibility.Collapsed;
            }


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
           MostrarCustomMessageBox();  
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom(); // Establece la ventana principal como propietaria para mantener el enfoque.
            customMessageBox.ShowDialog(); // Muestra el MessageBox personalizado como un cuadro de diálogo modal.
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
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Ingrese valor válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
            
            }

            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si el primer elemento está seleccionado (índice 0)
            if (cbInstructor.SelectedIndex == 0)
            {
                // Mostrar el TextBox y ocultar el ComboBox
                txtcbInstructor.Visibility = Visibility.Visible;
                cbInstructor.Visibility = Visibility.Collapsed;
                txtcbInstructor.Focus();
            }
            else
            {
                // Ocultar el TextBox y mostrar el ComboBox
                txtcbInstructor.Visibility = Visibility.Hidden;
                cbInstructor.Visibility = Visibility.Visible;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Verificar si el TextBox está vacío al perder el enfoque
            if (string.IsNullOrWhiteSpace(txtcbInstructor.Text))
            {
                // Mostrar el ComboBox y ocultar el TextBox
                txtcbInstructor.Visibility = Visibility.Hidden;
                cbInstructor.Visibility = Visibility.Visible;
                cbInstructor.SelectedIndex = 1;
            }
        }
     
        private void dtTermina_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime? selectedDateT = dtTermina.SelectedDate;


            if (selectedDateT<selectedDate) 
            {
                errTer.Content = "No puede ser menor a la inicial";
                errTer.Visibility = Visibility.Visible;
            }
            else
            {
                errTer.Visibility = Visibility.Collapsed;
            }
        }

        private void txtDuracion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDuracion.Text))
            {
                txtDuracion.BorderBrush = bordeError;
                errDur.Content = req; 
                errDur.Visibility = Visibility.Visible;
            }
            else
            {
                errDur.Visibility = Visibility.Collapsed;
                txtDuracion.BorderBrush = bordeNormal;
            }
        }

        private void txtLugar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLugar.Text))
            {
                txtLugar.BorderBrush = bordeError;
                errLug.Content = req;
                errLug.Visibility = Visibility.Visible;
            }
            else
            {
                errLug.Visibility = Visibility.Collapsed;
                txtLugar.BorderBrush = bordeNormal;
            }
        }

        private void dtInicia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime fechaActual = DateTime.Now;
            DateTime fechaAnterior = fechaActual.AddDays(-1);

            if (selectedDate < fechaAnterior)
            {
                // La fecha seleccionada es anterior a la fecha actual
                errIn.Content = "No puede ser anterior a la actual";
                errIn.Visibility = Visibility.Visible;

            }
            else
            {
                errIn.Visibility = Visibility.Collapsed;
            }
           
        }

        private void TiHorario_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            DateTime? horaSeleccionada = tiHorario.SelectedTime;

            if (horaSeleccionada.HasValue)
            {
                DateTime horaActual = DateTime.Now;

                // Comparar la hora seleccionada con la hora actual
                if (horaSeleccionada.Value < horaActual)
                {
                    // La hora seleccionada es anterior a la hora actual, mostrar el mensaje de error
                    errHor.Content = "No puede ser anterior a la hora actual.";
                    errHor.Visibility = Visibility.Visible;
                }
                else
                {
                    errHor.Visibility = Visibility.Collapsed;
                }
            }            
        }
    }
}

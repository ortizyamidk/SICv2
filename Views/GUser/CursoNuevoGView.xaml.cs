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
using static WPF_LoginForm.Views.GUser.CursoNuevoGView;

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

        private ObservableCollection<Participante> participantes;

        
        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            // Suscribir al evento SelectionChanged del ComboBox
            cbInstructor.SelectionChanged += ComboBox_SelectionChanged;
            // Suscribir al evento LostFocus del TextBox
            txtcbInstructor.LostFocus += TextBox_LostFocus;

            //insertar fechas actuales
            dtInicia.SelectedDate = DateTime.Now;
            dtTermina.SelectedDate = DateTime.Now;

            tiHorario.SelectedTime = DateTime.Now;

            // Suscribir al evento SelectionChanged del TimePicker
            tiHorario.SelectedTimeChanged += TiHorario_SelectedTimeChanged;

        }
 
        public class Participante
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errLug.Content = string.Empty;
            errDur.Content = string.Empty;

            txtDuracion.BorderBrush = bordeNormal;
            txtLugar.BorderBrush = bordeNormal;


            if (string.IsNullOrEmpty(txtDuracion.Text))
            {
                errDur.Content = req;
                txtDuracion.BorderBrush = bordeError;
                errores = true;
            }

            if (string.IsNullOrEmpty(txtLugar.Text))
            {
                errLug.Content = req;
                txtLugar.BorderBrush = bordeError;
                errores = true;
            }

            if (participantesDataGrid.Items.Count == 0)
            {
                MessageBox.Show("La lista no contiene registros. Agregue al menos un participante.", "No hay registros", MessageBoxButton.OK, MessageBoxImage.Error);
                errores = true;
            }

            if (!errores)
            {                
                MostrarCustomMessageBox();
                limpiar();
            }

        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom(); 
            customMessageBox.ShowDialog(); 
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
                MessageBox.Show("Ingrese un No. de ficha", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                var converter = new BrushConverter();
                // Inicializa la colección de participantes
                participantes = new ObservableCollection<Participante>();

                participantes.Add(new Participante { num = "1", nombre = "Marcella Yamilet Ortiz Guillén", puesto = "Analista de Laboratorio Insp. Recibo de Comp" });
                /*participantes.Add(new Participante { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
                participantes.Add(new Participante { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
                participantes.Add(new Participante { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
                participantes.Add(new Participante { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
                participantes.Add(new Participante { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
                participantes.Add(new Participante { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
                participantes.Add(new Participante { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
                participantes.Add(new Participante { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
                participantes.Add(new Participante { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });*/

                participantesDataGrid.ItemsSource = participantes;

                txtBuscar.Text = string.Empty;
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

            if (selectedDate.HasValue)
            {
                if (selectedDateT<selectedDate) 
                {
                    errTer.Content = "Debe ser menor a la inicial";
                    errTer.Visibility = Visibility.Visible;
                    btnGuardar.IsEnabled= false;
                }
                else
                {
                    errTer.Visibility = Visibility.Collapsed;
                    btnGuardar.IsEnabled = true;
                }

                dtTermina.BorderBrush = bordeNormal;
            }
            else
            {
                btnGuardar.IsEnabled = false;
                errTer.Content = req;
                errTer.Visibility = Visibility.Visible;
                dtTermina.BorderBrush = bordeError;
            }                                                          
        }       

        private void dtInicia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime fechaActual = DateTime.Now;
            DateTime fechaAnterior = fechaActual.AddDays(-1);

            if (selectedDate.HasValue)
            {
                if (selectedDate < fechaAnterior)
                {
                    // La fecha seleccionada es anterior a la fecha actual
                    errIn.Content = "Debe ser posterior a la actual";
                    errIn.Visibility = Visibility.Visible;
                    btnGuardar.IsEnabled = false;

                }
                else
                {
                    errIn.Visibility = Visibility.Collapsed;
                    btnGuardar.IsEnabled = true;
                }

                dtInicia.BorderBrush = bordeNormal;
            }
            else
            {
                btnGuardar.IsEnabled = false;
                errIn.Content = req;
                errIn.Visibility = Visibility.Visible;
                dtInicia.BorderBrush = bordeError;
            }
            
           
        }

        private void TiHorario_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            DateTime? horaSeleccionada = tiHorario.SelectedTime;

            if (horaSeleccionada.HasValue)
            {                
              btnGuardar.IsEnabled = true;
              errHor.Visibility = Visibility.Collapsed;
              tiHorario.BorderBrush = bordeNormal;
            }
            else
            {                
                btnGuardar.IsEnabled= false;
                errHor.Content = req;
                errHor.Visibility= Visibility.Visible;
                tiHorario.BorderBrush= bordeError;                
            }
        }

        public void limpiar()
        {
            // Restablecer los valores de los controles a sus valores iniciales
            txtDuracion.Text = string.Empty;
            txtLugar.Text = string.Empty;
            dtInicia.SelectedDate = DateTime.Now;
            dtTermina.SelectedDate = DateTime.Now;
            tiHorario.SelectedTime = DateTime.Now;
            cbInstructor.SelectedIndex = 0;
            txtcbInstructor.Text = string.Empty;

            // Restablecer los bordes y etiquetas de error
            txtDuracion.BorderBrush = bordeNormal;
            txtLugar.BorderBrush = bordeNormal;
            errDur.Content = string.Empty;
            errLug.Content = string.Empty;
            errIn.Content = string.Empty;
            errTer.Content = string.Empty;
            errHor.Content = string.Empty;

            dtInicia.Focus();

            participantes.Clear();            
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que se hizo clic
            Button button = sender as Button;

            if (button != null)
            {
                // Obtener el elemento de la fila seleccionada
                var participante = button.DataContext as Participante;

                if (participante != null)
                {
                    // Eliminar el elemento de la colección de datos
                    participantes.Remove(participante);
                }
            }
        }
    }
}

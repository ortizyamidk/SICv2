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
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WPF_LoginForm.Views
{

    public partial class CursoNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        string id, nombrecurso, inicia, termina, horario, lugar;
        int duracion;
        string area, instructor;

        CursoRepository cursoRepository;
        InstructorRepository instructorRepository;

        public CursoNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            cursoRepository = new CursoRepository();
            instructorRepository = new InstructorRepository();

            cbArea.SelectionChanged += ComboBox_SelectionChanged;
            txtcbArea.LostFocus += TextBox_LostFocus;

            CargarInstructores();
            cbInstructor.SelectionChanged += ComboBox_SelectionChanged;
            txtcbInstructor.LostFocus += TextBox_LostFocus;

            dtInicia.SelectedDate = DateTime.Now;
            dtTermina.SelectedDate = DateTime.Now;
            tiHorario.SelectedTime = DateTime.Now;

            tiHorario.SelectedTimeChanged += TiHorario_SelectedTimeChanged;           

        }

        private void CargarInstructores()
        {
            var instructores = instructorRepository.GetByAll();
            foreach (var instructor in instructores)
            {
                var item = new ComboBoxItem
                {
                    Content = instructor.NomInstr
                };
                cbInstructor.Items.Add(item);
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; // Obtener el ComboBox que disparó el evento

            // Verificar si el primer elemento está seleccionado (índice 0)
            if (comboBox.SelectedIndex == 0)
            {
                // Mostrar el TextBox y ocultar el ComboBox
                TextBox textBox = null;

                if (comboBox == cbArea)
                {
                    textBox = txtcbArea;
                }
                else if (comboBox == cbInstructor)
                {
                    textBox = txtcbInstructor;
                }

                if (textBox != null)
                {
                    textBox.Visibility = Visibility.Visible;
                    comboBox.Visibility = Visibility.Collapsed;
                    textBox.Focus();
                }
            }
            else
            {
                // Ocultar el TextBox y mostrar el ComboBox
                if (comboBox == cbArea)
                {
                    txtcbArea.Visibility = Visibility.Hidden;
                    cbArea.Visibility = Visibility.Visible;
                }
                else if (comboBox == cbInstructor)
                {
                    txtcbInstructor.Visibility = Visibility.Hidden;
                    cbInstructor.Visibility = Visibility.Visible;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox; // Obtener el TextBox que disparó el evento

            if (textBox == txtcbArea)
            {
                if (string.IsNullOrWhiteSpace(txtcbArea.Text))
                {
                    // Mostrar el ComboBox y ocultar el TextBox
                    txtcbArea.Visibility = Visibility.Hidden;
                    cbArea.Visibility = Visibility.Visible;
                    cbArea.SelectedIndex = 1;
                }
            }
            else if (textBox == txtcbInstructor)
            {
                if (string.IsNullOrWhiteSpace(txtcbInstructor.Text))
                {
                    // Mostrar el ComboBox y ocultar el TextBox
                    txtcbInstructor.Visibility = Visibility.Hidden;
                    cbInstructor.Visibility = Visibility.Visible;
                    cbInstructor.SelectedIndex = 1;
                }
            }
        }

        private void dtTermina_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime? selectedDateT = dtTermina.SelectedDate;

            if (selectedDate.HasValue)
            {
                if (selectedDateT < selectedDate)
                {
                    errTer.Content = "Debe ser mayor a la inicial";
                    errTer.Visibility = Visibility.Visible;
                    btnGuardar.IsEnabled = false;
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
                btnGuardar.IsEnabled = false;
                errHor.Content = req;
                errHor.Visibility = Visibility.Visible;
                tiHorario.BorderBrush = bordeError;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Focus();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            errLug.Content = string.Empty;
            errDur.Content = string.Empty;
            errCurso.Content = string.Empty;
            errID.Content = string.Empty;

            txtDuracion.BorderBrush = bordeNormal;
            txtLugar.BorderBrush = bordeNormal;
            txtNombreC.BorderBrush = bordeNormal;
            txtID.BorderBrush = bordeNormal;

            
            if (string.IsNullOrEmpty(txtID.Text))
            {
                errID.Content = req;
                txtID.BorderBrush = bordeError;
                errores = true;
            }

            var existingCurso = cursoRepository.GetByAll().FirstOrDefault(c => c.Id == txtID.Text);
            var existingCurso2 = cursoRepository.GetByAll().FirstOrDefault(c => c.NomCurso == txtNombreC.Text);

            if (existingCurso != null)
            {
                MessageBox.Show("Ya existe ese ID del Curso", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                errores = true;
            }
            if (existingCurso2 != null)
            {
                MessageBox.Show("No puede haber 2 cursos con el mismo nombre", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                errores = true;
            }

            if (string.IsNullOrEmpty(txtNombreC.Text))
            {
                errCurso.Content = req;
                txtNombreC.BorderBrush = bordeError;
                errores = true;
            }

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

            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime? selectedDateT = dtTermina.SelectedDate;
            if (selectedDateT < selectedDate)
            {
                errores = true;
            }

            DateTime fechaActual = DateTime.Now;
            DateTime fechaAnterior = fechaActual.AddDays(-1);
            if (selectedDate < fechaAnterior)
            {
                errores = true;
            }

            string selectedinicia = selectedDate.ToString();
            string selectedtermina = selectedDateT.ToString();
            DateTime? horaSeleccionada = tiHorario.SelectedTime;
            string selectedhora = horaSeleccionada.ToString();

            if (string.IsNullOrEmpty(selectedinicia)||string.IsNullOrEmpty(selectedtermina)||string.IsNullOrEmpty(selectedhora))
            {
                errores = true;
            }

            if (!errores)
            {
                id = txtID.Text;
                nombrecurso = txtNombreC.Text;
                inicia = dtInicia.SelectedDate.ToString();
                termina = dtTermina.SelectedDate.ToString();
                horario = tiHorario.SelectedTime.ToString();
                duracion = int.Parse(txtDuracion.Text);
                lugar = txtLugar.Text;


                // Verificar el índice seleccionado en cbArea
                if (cbArea.SelectedIndex == 0)
                {
                    area = txtcbArea.Text; //escribe un area en especifico
                }
                else if (cbArea.SelectedIndex > 0)
                {
                    ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
                    area = areaseleccionada.Content.ToString(); //toma un area que ya existe
                }

                // Verificar el índice seleccionado en cbInstructor
                if (cbInstructor.SelectedIndex == 0)
                {
                    instructor = txtcbInstructor.Text;
                    cursoRepository.AddCursoInstructorTemporal(id, nombrecurso, area, inicia, termina, horario, duracion, lugar, instructor); //insertar si se escribe un instructor temporal
                }
                else if (cbInstructor.SelectedIndex > 0)
                {
                    ComboBoxItem selectedItemInst = (ComboBoxItem)cbInstructor.SelectedItem;
                    instructor = selectedItemInst.Content.ToString();

                    InstructorModel instridModel = instructorRepository.GetIdByNombre(instructor);
                    int instrid = instridModel.Id;
                    cursoRepository.AddCursoInstructor(id, nombrecurso, area, inicia, termina, horario, duracion, lugar, instrid); //insertar si se selecciona un instructor que ya existe

                }

                var areaRepository = new AreaRepository();
                var areaIds = areaRepository.GetIdAreasRegistran();

                foreach (var areaId in areaIds)
                {
                    int idarea = areaId.Id;

                    cursoRepository.AddCursoArea(idarea, id); //permite asignarle el curso a todas las areas que registran
                }

                MostrarCustomMessageBox();
                Limpiar();
            }          
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Llama al manejador de eventos del botón btnSearch.
                btnGuardar_Click(sender, e);
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
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

        private void Limpiar()
        {
            errCurso.Content = string.Empty;
            txtNombreC.BorderBrush = bordeNormal;
            txtNombreC.Text = string.Empty;
            txtID.Focus();
            cbArea.SelectedIndex = 1;

            // Restablecer los valores de los controles a sus valores iniciales
            txtDuracion.Text = string.Empty;
            txtLugar.Text = string.Empty;
            dtInicia.SelectedDate = DateTime.Now;
            dtTermina.SelectedDate = DateTime.Now;
            tiHorario.SelectedTime = DateTime.Now;
            cbInstructor.SelectedIndex = 1;
            txtcbInstructor.Text = string.Empty;
            txtID.Text = string.Empty;

            // Restablecer los bordes y etiquetas de error
            txtDuracion.BorderBrush = bordeNormal;
            txtLugar.BorderBrush = bordeNormal;
            errDur.Content = string.Empty;
            errLug.Content = string.Empty;
            errIn.Content = string.Empty;
            errTer.Content = string.Empty;
            errHor.Content = string.Empty;
        }
    }
}

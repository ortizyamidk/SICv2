using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class CursoInfoView : UserControl
    {

        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        string id, nombrecurso, inicia, termina, horario, lugar;
        int duracion;
        string area, instructor;

        CursoRepository cursoRepository;
        InstructorRepository instructorRepository;

        public CursoInfoView()
        {
            InitializeComponent();

            deshabilitar();

            cursoRepository = new CursoRepository();
            instructorRepository = new InstructorRepository();

            CursoModel curso = cursoRepository.GetById("CU1"); //obtener curso

            txtID.Text = curso.Id.ToString();
            txtNombreC.Text = curso.NomCurso.ToString();
            txtDuracion.Text = curso.Duracion.ToString();
            txtLugar.Text = curso.Lugar.ToString();

            string fechaInicioString = curso.Inicio.ToString();
            string fechaTermString = curso.Termino.ToString();
            string horarioString = curso.Horario.ToString();

            string areatemString = curso.AreaTematica.ToString();
            string instructorString = curso.Instructor.ToString();

            DateTime fechaInicio = DateTime.ParseExact(fechaInicioString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtInicia.SelectedDate = fechaInicio;

            DateTime fechaTermino = DateTime.ParseExact(fechaTermString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtTermina.SelectedDate = fechaTermino;

            DateTime hor = DateTime.Parse(horarioString);
            tiHorario.SelectedTime = hor;

            //
            // Aquí buscas el índice en el ComboBox correspondiente
            int areaIndex = -1; // Inicializas en -1 para saber si encontraste coincidencia
            for (int i = 0; i < cbArea.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)cbArea.Items[i];
                if (item.Content.ToString() == areatemString)
                {
                    areaIndex = i;
                    break; // Encuentras coincidencia, sales del bucle
                }
            }
            if (areaIndex == -1)
            {
                // No se encontró coincidencia, seleccionas el primer elemento
                areaIndex = 0;
                txtcbArea.Text = areatemString; // Insertas el texto en el TextBox
                ComboBoxItem item = (ComboBoxItem)cbArea.Items[0];
                item.Content = areatemString;
            }
            cbArea.SelectedIndex = areaIndex; // Estableces el índice del ComboBox


            //

            CargarInstructores();
            int instructorIndex = -1; // Inicializas en -1 para saber si encontraste coincidencia
            for (int i = 0; i < cbInstructor.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)cbInstructor.Items[i];
                if (item.Content.ToString() == instructorString)
                {
                    instructorIndex = i;
                    break; // Encuentras coincidencia, sales del bucle
                }
            }
            if (instructorIndex == -1)
            {
                // No se encontró coincidencia, seleccionas el primer elemento
                instructorIndex = 0;
                txtcbInstructor.Text = instructorString; // Insertas el texto en el TextBox
                ComboBoxItem item2 = (ComboBoxItem)cbInstructor.Items[0];
                item2.Content = instructorString;
            }
            cbInstructor.SelectedIndex = instructorIndex; // Estableces el índice del ComboBox
            //

            cbArea.SelectionChanged += ComboBox_SelectionChanged;
            txtcbArea.LostFocus += TextBox_LostFocus;

            cbInstructor.SelectionChanged += ComboBox_SelectionChanged;
            txtcbInstructor.LostFocus += TextBox_LostFocus;

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
                    btnSave.IsEnabled = false;
                }
                else
                {
                    errTer.Visibility = Visibility.Collapsed;
                    btnSave.IsEnabled = true;
                }

                dtTermina.BorderBrush = bordeNormal;
            }
            else
            {
                btnSave.IsEnabled = false;
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
                    btnSave.IsEnabled = false;

                }
                else
                {
                    errIn.Visibility = Visibility.Collapsed;
                    btnSave.IsEnabled = true;
                }

                dtInicia.BorderBrush = bordeNormal;
            }
            else
            {
                btnSave.IsEnabled = false;
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
                btnSave.IsEnabled = true;
                errHor.Visibility = Visibility.Collapsed;
                tiHorario.BorderBrush = bordeNormal;
            }
            else
            {
                btnSave.IsEnabled = false;
                errHor.Content = req;
                errHor.Visibility = Visibility.Visible;
                tiHorario.BorderBrush = bordeError;
            }
        }

        public void habilitar()
        {
            
            txtNombreC.IsEnabled = true;
            cbArea.IsEnabled = true;
            txtcbArea.IsEnabled = true;
            dtInicia.IsEnabled = true;
            dtTermina.IsEnabled = true;
            tiHorario.IsEnabled = true;
            txtDuracion.IsEnabled = true;
            txtLugar.IsEnabled = true;
            cbInstructor.IsEnabled = true;
            txtcbInstructor.IsEnabled = true;

            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;

            txtID.Focus();
        }

        public void deshabilitar()
        {
            txtID.IsEnabled = false;
            txtNombreC.IsEnabled= false;
            cbArea.IsEnabled = false;
            txtcbArea.IsEnabled = false;
            dtInicia.IsEnabled = false;
            dtTermina.IsEnabled = false;
            tiHorario.IsEnabled = false;
            txtDuracion.IsEnabled = false;
            txtLugar.IsEnabled = false;
            cbInstructor.IsEnabled = false;
            txtcbInstructor.IsEnabled = false;

            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            habilitar();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
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
                    area = txtcbArea.Text;
                }
                else if (cbArea.SelectedIndex > 0)
                {
                    ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
                    area = areaseleccionada.Content.ToString();
                }

                // Verificar el índice seleccionado en cbInstructor
                if (cbInstructor.SelectedIndex == 0)
                {
                    instructor = txtcbInstructor.Text;
                    cursoRepository.EditCursoITemporal(nombrecurso, area, inicia, termina, horario, duracion, lugar, instructor, id);

                }
                else if (cbInstructor.SelectedIndex > 0)
                {
                    ComboBoxItem selectedItemInst = (ComboBoxItem)cbInstructor.SelectedItem;
                    instructor = selectedItemInst.Content.ToString();

                    InstructorModel instridModel = instructorRepository.GetIdByNombre(instructor);
                    int instrid = instridModel.Id;

                    cursoRepository.EditCurso(nombrecurso, area, inicia, termina, horario, duracion, lugar, instrid, id);
                }               

                MostrarCustomMessageBox();
                deshabilitar();
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

    }
}

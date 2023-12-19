using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

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
        AreaRepository areaRepository;

        private ObservableCollection<AreaModel> areasAgregadas = new ObservableCollection<AreaModel>();

        public CursoNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            cursoRepository = new CursoRepository();
            instructorRepository = new InstructorRepository();
            areaRepository = new AreaRepository();           
            

            cbArea.SelectionChanged += ComboBox_SelectionChanged;
            txtcbArea.LostFocus += TextBox_LostFocus;

            
            cbInstructor.SelectionChanged += ComboBox_SelectionChanged;
            txtcbInstructor.LostFocus += TextBox_LostFocus;

            dtInicia.SelectedDate = DateTime.Now;
            dtTermina.SelectedDate = DateTime.Now;
            tiHorario.SelectedTime = DateTime.Now;

            tiHorario.SelectedTimeChanged += TiHorario_SelectedTimeChanged;           
        }

        private void CargarAreas()
        {
            try
            {
                var areas = areaRepository.GetByAll();
                foreach (var area in areas)
                {
                    var item = new ComboBoxItem
                    {
                        Content = area.NombreArea
                    };
                    cbAreaDpto.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CargarInstructores()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox; // Obtener el ComboBox que disparó el evento

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox; // Obtener el TextBox que disparó el evento

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void dtTermina_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime? selectedDateT = dtTermina.SelectedDate;

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void dtInicia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtInicia.SelectedDate;
            DateTime fechaActual = DateTime.Now;

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            CargarInstructores();
            CargarAreas();

            // Asignar el origen de datos para el DataGrid
            areasCursoDataGrid.ItemsSource = areasAgregadas;

            // Eliminar la selección predeterminada del ComboBox al cargar la página
            cbAreaDpto.SelectedIndex = -1;

            // Asegurarse de que el DataGrid esté vacío al inicio
            areasAgregadas.Clear();
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

            try
            {
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
                if (areasAgregadas.Count <= 0)
                {
                    errores=true;
                    MessageBox.Show("Agregue las áreas a las que va dirigido el curso");
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

                    // Obtener los nombres de las áreas de la colección
                    List<string> nombresAreas = areasAgregadas.Select(area => area.NombreArea).ToList();

                    // Obtener los IDs de las áreas por nombre
                    foreach (string nombreArea in nombresAreas)
                    {
                        var areaIds = areaRepository.GetIdsAreasByName(nombreArea);

                        foreach (var areaId in areaIds)
                        {
                            int idarea = areaId.Id;

                            // Asignar el curso a cada área encontrada por su ID
                            cursoRepository.AddCursoArea(idarea, id);
                        }
                    }

                    MostrarCustomMessageBox();
                    Limpiar();
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

                    
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGuardar_Click(sender, e);
            }
        }

        private void cbAreaDpto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox.SelectedItem != null)
                {
                    ComboBoxItem selectedArea = (ComboBoxItem)cbAreaDpto.SelectedItem;
                    string areaSeleccionada = selectedArea.Content.ToString();

                    // Verificar si el área seleccionada ya está en el DataGrid
                    bool areaExistente = areasAgregadas.Any(area => area.NombreArea == areaSeleccionada);

                    if (!areaExistente)
                    {
                        // Agregar el área seleccionada al DataGrid (lista temporal)
                        areasAgregadas.Add(new AreaModel { NombreArea = areaSeleccionada });
                        areasCursoDataGrid.ItemsSource = areasAgregadas;
                    }

                    // Eliminar el área seleccionada del ComboBox si ya está en el DataGrid
                    if (areaExistente)
                    {
                        cbAreaDpto.Items.Remove(selectedArea);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (areasCursoDataGrid.SelectedItem != null)
                {
                    AreaModel selectedArea = areasCursoDataGrid.SelectedItem as AreaModel;

                    // Eliminar el área seleccionada del DataGrid
                    areasAgregadas.Remove(selectedArea);

                    // Agregar el área eliminada de vuelta al ComboBox si no está presente
                    bool areaExistente = cbAreaDpto.Items.Cast<ComboBoxItem>().Any(item => item.Content.ToString() == selectedArea.NombreArea);

                    if (!areaExistente)
                    {
                        cbAreaDpto.Items.Add(new ComboBoxItem { Content = selectedArea.NombreArea });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            // Limpiar la colección de áreas agregadas en el DataGrid
            areasAgregadas.Clear();
            areasCursoDataGrid.ItemsSource = areasAgregadas;

            // Reiniciar el ComboBox cbAreaDpto cargando las áreas nuevamente
            cbAreaDpto.Items.Clear(); // Limpia los elementos actuales del ComboBox

            // Vuelve a cargar las áreas en el ComboBox
            CargarAreas();
        }
    }
}

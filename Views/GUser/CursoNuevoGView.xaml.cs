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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using static WPF_LoginForm.Views.CursosView;
using static WPF_LoginForm.Views.GUser.CursoGView;
using static WPF_LoginForm.Views.GUser.CursoNuevoGView;

namespace WPF_LoginForm.Views.GUser
{
    public partial class CursoNuevoGView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";
        string inicia, termina, horario, lugar;
        int duracion;
        string nombrecurso;

        CursoGRepository repository;
        InstructorRepository instructorRepository;
        TrabajadorRepository trabajadorRepository;

        ObservableCollection<TrabajadorModel> trabajadoresList = new ObservableCollection<TrabajadorModel>();


        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            repository = new CursoGRepository();
            instructorRepository = new InstructorRepository();
            trabajadorRepository = new TrabajadorRepository();

            trabajadoresList = new ObservableCollection<TrabajadorModel>();

            CargarCursos();
            CargarInstructores();

            ComboBoxItem selectedItem = (ComboBoxItem)cbCurso.SelectedItem;
            nombrecurso = selectedItem.Content.ToString();

            CursoGModel areacurso = repository.GetIDCursoByNombre(nombrecurso);            
            txtArea.Text = areacurso.AreaTematica;

            cbInstructor.SelectionChanged += ComboBox_SelectionChanged;
            txtcbInstructor.LostFocus += TextBox_LostFocus;

            dtInicia.SelectedDate = DateTime.Now;
            dtTermina.SelectedDate = DateTime.Now;
            tiHorario.SelectedTime = DateTime.Now;

            tiHorario.SelectedTimeChanged += TiHorario_SelectedTimeChanged;
        }

        private void CargarCursos()
        {
            var cursos = repository.GetCursos("Calidad");

            cbCurso.Items.Clear();

            foreach (var curso in cursos)
            {
                var item = new ComboBoxItem
                {
                    Content = curso.NomCurso
                };
                cbCurso.Items.Add(item);
            }
           
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
            if (cbCurso.Items.Count == 0)
            {
                errores = true;
                dtInicia.IsEnabled = false;
                dtTermina.IsEnabled = false;
                tiHorario.IsEnabled = false;
                txtDuracion.IsEnabled = false;
                txtLugar.IsEnabled = false;
                cbInstructor.IsEnabled = false;
                txtcbInstructor.IsEnabled = false;
                btnSearch.IsEnabled = false;
                txtBuscar.IsEnabled = false;

                limpiar();
                MessageBox.Show("No hay cursos a registrar");
            }

            if (!errores)
            {
                inicia = dtInicia.SelectedDate.ToString();
                termina = dtTermina.SelectedDate.ToString();
                horario = tiHorario.SelectedTime.ToString();
                duracion = int.Parse(txtDuracion.Text);
                lugar = txtLugar.Text;
                

               CursoGModel cursoidModel = repository.GetIDCursoByNombre(nombrecurso);             
               int cursoid = cursoidModel.Id;
               
                //insertar en tabla lista cursos
                repository.AddListaAsistencia(inicia, termina, horario, duracion, lugar, cursoid);
                //editar tabla cursos de que ya fue registrado
                repository.IsCursoRegistered(cursoid);

                if (cbInstructor.SelectedIndex==0)
                {
                    string instructortemporal = txtcbInstructor.Text;

                    repository.AddInstructorTemporal(instructortemporal, cursoid);
                }
                else
                {
                    ComboBoxItem selectedItemInst = (ComboBoxItem) cbInstructor.SelectedItem;
                    string nombreinstructor = selectedItemInst.Content.ToString();
                    InstructorModel instridModel = instructorRepository.GetIdByNombre(nombreinstructor);
                    int instrid = instridModel.Id;

                    MessageBox.Show("Instructor: "+ instrid);

                    //editar tabla cursos para agregar instructor
                    repository.AddInstructor(instrid, cursoid);
                }                              

                CursoGModel lastidlista = repository.GetLastIdLista();
                int lastid = lastidlista.Id;

                MostrarCustomMessageBox();

                // Recorrer la colección trabajadoresList y agregar a cada trabajador al mismo curso
                foreach (var participante in trabajadoresList)
                {
                    repository.AddParticipantes(participante.Id, lastid);
                }

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
               int numficha = int.Parse(txtBuscar.Text);
               
               TrabajadorModel trabajador = trabajadorRepository.GetById(numficha);

                if (trabajador!=null)
                {
                    // Verificar si ya existe un trabajador con el mismo ID en la lista
                    if (!trabajadoresList.Any(t => t.Id == numficha))
                    {
                        // Agregar trabajador a la ObservableCollection
                        trabajadoresList.Add(trabajador);

                        // Actualizar el DataGrid
                        participantesDataGrid.ItemsSource = trabajadoresList;
                    }
                    else
                    {
                        MessageBox.Show("Trabajador ya agregado", "Duplicado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("No existe Trabajador con ese Num. de ficha", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                txtBuscar.Text = string.Empty;
            }

            txtBuscar.Focus();          
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

            trabajadoresList.Clear();            
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que se hizo clic
            Button button = sender as Button;

            if (button != null)
            {
                // Obtener el elemento de la fila seleccionada
                var participante = button.DataContext as TrabajadorModel;

                if (participante != null)
                {
                    // Eliminar el elemento de la colección de datos
                    trabajadoresList.Remove(participante);
                }
            }
        }
    }
}

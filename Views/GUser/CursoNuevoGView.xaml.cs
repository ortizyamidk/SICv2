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
using System.Windows.Controls.Primitives;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static WPF_LoginForm.Views.CursosView;
using static WPF_LoginForm.Views.GUser.CursoGView;
using static WPF_LoginForm.Views.GUser.CursoNuevoGView;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views.GUser
{
    public partial class CursoNuevoGView : UserControl
    {
        CursoGRepository repository;
        TrabajadorRepository trabajadorRepository;
        AreaRepository areaRepository;
        CursoRepository cursoRepository;

        ObservableCollection<TrabajadorModel> trabajadoresList = new ObservableCollection<TrabajadorModel>();

        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;          

            repository = new CursoGRepository();
            trabajadorRepository = new TrabajadorRepository();
            areaRepository = new AreaRepository();
            cursoRepository = new CursoRepository();

            trabajadoresList = new ObservableCollection<TrabajadorModel>();

            //traer y guardar el nombre del curso seleccionado en la tarjeta de Home, hacer una comparacion con los cursos que existen y seleccionar ese index
            //string cursoSelecTarjeta;

            //si el valor es nulo, entonces...
            cbCurso.SelectedIndex = 0;
           
        }

        private void CargarCursos()
        {
            var viewModel = (CursoNuevoGViewModel)DataContext;
            var cursos = cursoRepository.GetCursosNotRegistered(viewModel.CurrentUserAccount.DisplayArea); //obtener area loggeada
            foreach (var curso in cursos)
            {
                var item = new ComboBoxItem
                {
                    Content = curso.NomCurso
                };

                cbCurso.Items.Add(item);
            }
        }
    

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            if (participantesDataGrid.Items.Count == 0)
            {
                MessageBox.Show("La lista no contiene registros. Agregue al menos un participante.", "No hay registros", MessageBoxButton.OK, MessageBoxImage.Error);
                errores = true;
            }                      

            if (!errores)
            {                                                        
                string idcurso = txtID.Text;               

                // Recorrer la colección trabajadoresList y agregar a cada trabajador al mismo curso
                foreach (var participante in trabajadoresList)
                {
                    repository.AddParticipantes(participante.Id, idcurso);
                }

                var viewModel = (CursoNuevoGViewModel)DataContext;
                AreaModel areaModel = areaRepository.GetIdByName(viewModel.CurrentUserAccount.DisplayArea); //area loggeada
                int idCurrentArea = areaModel.Id;

                repository.AddListaAsistencia(idCurrentArea, idcurso);

                MostrarCustomMessageBox();
                btnGuardar.IsEnabled = false;
                CargarCursos();
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom(); 
            customMessageBox.ShowDialog(); 
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtBuscar.Focus();
            CargarCursos();

            if (cbCurso.Items.Count == 0)
            {
                MessageBox.Show("No hay cursos por registrar.", "Sin cursos para el mes", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtBuscar.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                btnSearch.IsEnabled = false;
            }
        }

        private void cbCurso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Capturar el evento de selección de elementos en el ComboBox
            if (cbCurso.SelectedItem != null)
            {
                ComboBoxItem cursoSeleccionado = (ComboBoxItem)cbCurso.SelectedItem;
                string cursoSeleccionadoStr = cursoSeleccionado.Content.ToString();

                CursoModel curso = cursoRepository.GetByName(cursoSeleccionadoStr);
                txtID.Text = curso.Id;
                txtArea.Text = curso.AreaTematica;
                txtLugar.Text = curso.Lugar;
                txtInicia.Text = curso.Inicio;
                txtTermina.Text = curso.Termino;
                txtHor.Text = curso.Horario;
                txtDuracion.Text = curso.Duracion.ToString();
                txtInstr.Text = curso.Instructor;

                int cursoIndex = -1;
                for (int i = 0; i < cbCurso.Items.Count; i++)
                {
                    ComboBoxItem item = (ComboBoxItem)cbCurso.Items[i];
                    if (item.Content.ToString() == curso.NomCurso)
                    {
                        cursoIndex = i;
                        break;
                    }
                }
                cbCurso.SelectedIndex = cursoIndex;
            }
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Ingrese un No. de ficha", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
               int numficha = int.Parse(txtBuscar.Text);

                var viewModel = (CursoNuevoGViewModel)DataContext;
                TrabajadorModel trabajador = trabajadorRepository.GetById(numficha, viewModel.CurrentUserAccount.DisplayArea); //area loggeada

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
                    MessageBox.Show("No existe Trabajador o no es del área de "+ viewModel.CurrentUserAccount.DisplayArea, "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                txtBuscar.Text = string.Empty;
            }

            txtBuscar.Focus();          
        }
      
        public void limpiar()
        {
            txtBuscar.Focus();
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

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}

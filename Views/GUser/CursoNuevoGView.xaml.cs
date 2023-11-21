using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views.GUser
{
    public partial class CursoNuevoGView : UserControl
    {
        CursoGRepository repository;
        TrabajadorRepository trabajadorRepository;
        AreaRepository areaRepository;
        CursoRepository cursoRepository;

        private bool isMessageBoxShown = false;

        ObservableCollection<TrabajadorModel> trabajadoresList;

        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;          

            repository = new CursoGRepository();
            trabajadorRepository = new TrabajadorRepository();
            areaRepository = new AreaRepository();
            cursoRepository = new CursoRepository();

            trabajadoresList = new ObservableCollection<TrabajadorModel>();                      
        }

        private void CargarCursos()
        {
            var viewModel = (CursoNuevoGViewModel)DataContext;

            try
            {
                var cursos = cursoRepository.GetCursosNotRegistered(viewModel.CurrentUserAccount.DisplayArea);
                foreach (var curso in cursos)
                {
                    var item = new ComboBoxItem
                    {
                        Content = curso.NomCurso
                    };

                    cbCurso.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }    

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (isMessageBoxShown) // Si ya se está mostrando un MessageBox, no hagas nada
            {
                return;
            }

            bool errores = false;

            if (participantesDataGrid.Items.Count == 0)
            {
                MessageBox.Show("La lista no contiene registros. Agregue al menos un participante.", "No hay registros", MessageBoxButton.OK, MessageBoxImage.Error);
                errores = true;
            }                      

            if (!errores)
            {
                isMessageBoxShown = true;

                MessageBoxResult result = MessageBox.Show("¿Está seguro de guardar la información? Los datos no se podrán editar al confirmar la acción", "Confirmar Guardar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if(result == MessageBoxResult.Yes)
                {
                    try
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
                        Limpiar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                isMessageBoxShown = false; // Indicar que se ha cerrado el MessageBox
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
            cbCurso.SelectedIndex = 0;

            if (cbCurso.Items.Count == 0)
            {
                MessageBox.Show("No hay cursos por registrar.", "Sin cursos para el mes", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtBuscar.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                btnSearch.IsEnabled = false;
                cbCurso.IsEnabled = false;
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
                try
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                txtBuscar.Text = string.Empty;
            }

            txtBuscar.Focus();          
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

        private void Limpiar()
        {
            txtID.Text = string.Empty;
            cbCurso.Items.Clear();
            txtArea.Text = string.Empty;
            txtLugar.Text = string.Empty;
            txtInicia.Text = string.Empty;
            txtTermina.Text = string.Empty;
            txtHor.Text = string.Empty;
            txtDuracion.Text = string.Empty;
            txtInstr.Text = string.Empty;
            trabajadoresList.Clear();

            CargarCursos();
            cbCurso.SelectedIndex = 0;
            txtBuscar.Focus();

            if (cbCurso.Items.Count == 0)
            {
                MessageBox.Show("No hay cursos por registrar.", "Sin cursos para el mes", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtBuscar.IsEnabled = false;
                btnGuardar.IsEnabled = false;
                btnSearch.IsEnabled = false;
            }
        }
    }
}

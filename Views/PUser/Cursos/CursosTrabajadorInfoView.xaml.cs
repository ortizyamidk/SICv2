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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using static WPF_LoginForm.Views.CursosView;

namespace WPF_LoginForm.Views
{
    
    public partial class CursosTrabajadorInfoView : UserControl
    {
        string idCurso;

        ObservableCollection<TrabajadorModel> participantes;

        
        DepartamentoRepository departamentoRepository;
        TrabajadorRepository trabajadorRepository;
        CursoGRepository cursoGRepository;




        public CursosTrabajadorInfoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            departamentoRepository = new DepartamentoRepository();
            trabajadorRepository = new TrabajadorRepository();
            cursoGRepository = new CursoGRepository();
            

        }

        private void CargarAreas()
        {
            var areas = departamentoRepository.GetDepartamentos();
            foreach (var area in areas)
            {
                var item = new ComboBoxItem
                {
                    Content = area.NomDepto
                };
                cbArea.Items.Add(item);
            }

         
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(); // Establece el enfoque en el TextBox
          
            CargarAreas();


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
                var repository = new CursoRepository();
                CursoGModel asistencia = repository.GetAsistenciaById(txtSearch.Text);

                // Restaurar la visibilidad y el estado inicial
                border.Visibility = Visibility.Collapsed;
                txtImpartido.Visibility = Visibility.Collapsed;
                txtImpartido.Text = string.Empty;

                if (asistencia != null)
                {                   

                    cbArea.SelectedIndex = 0;

                    txtIDCurso.Text = asistencia.IdCurso.ToString();
                    txtCurso.Text = asistencia.NomCurso.ToString();
                    txtArea.Text = asistencia.AreaTematica.ToString();
                    txtInicia.Text = asistencia.Inicia.ToString();
                    txtTerm.Text = asistencia.Termina.ToString();
                    txtHor.Text = asistencia.Horario.ToString();
                    txtDur.Text = asistencia.Duracion.ToString() + " min";
                    txtLugar.Text = asistencia.Lugar.ToString();
                    txtInst.Text = asistencia.Instructor.ToString();

                    idCurso = txtIDCurso.Text;

                    IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesById(idCurso);
                    participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                    cursosTrabajadorDataGrid.ItemsSource = participantes;

                    cbBorder.Visibility = Visibility.Visible;

                    int cursoimp = cursoGRepository.CursoImpartido(txtCurso.Text);

                    if (cursoimp == 1)
                    {
                        border.Visibility = Visibility.Visible;
                        txtImpartido.Visibility = Visibility.Visible;

                        SolidColorBrush colorImpartido = new SolidColorBrush(Colors.Green);
                        border.Background = colorImpartido;
                        txtImpartido.Text = "IMPARTIDO";
                    }
                    else if (cursoimp == 0)
                    {
                        border.Visibility = Visibility.Visible;
                        txtImpartido.Visibility = Visibility.Visible;

                        SolidColorBrush colorNOimpartido = new SolidColorBrush(Colors.Red);
                        border.Background = colorNOimpartido;
                        txtImpartido.Text = "NO IMPARTIDO";
                    }
                    else
                    {
                        border.Visibility = Visibility.Collapsed;
                        txtImpartido.Visibility = Visibility.Collapsed;
                        txtImpartido.Text = string.Empty;
                    }

                }
                else
                {
                    Limpiar();
                    MessageBox.Show("No existe curso con ese ID", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                
                txtSearch.Focus();
            }            
        }

        private void Limpiar()
        {
            txtIDCurso.Text = string.Empty;
            txtCurso.Text = string.Empty;
            txtArea.Text = string.Empty;
            txtInicia.Text = string.Empty;
            txtTerm.Text = string.Empty;
            txtHor.Text = string.Empty;
            txtDur.Text = string.Empty;
            txtLugar.Text = string.Empty;
            txtInst.Text = string.Empty;
            txtSearch.Text = string.Empty;
            participantes.Clear();
            border.Visibility= Visibility.Collapsed;
            cbBorder.Visibility= Visibility.Collapsed;
        }


        private void btnSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Llama al manejador de eventos del botón btnSearch.
                btnSearch_Click(sender, e);
            }
        }

        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            if (cbArea.SelectedIndex > 0)
            {               
                ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
                string area = areaseleccionada.Content.ToString();

                IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesByIdAndArea(idCurso, area);
                participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                cursosTrabajadorDataGrid.ItemsSource = participantes;
            }
        }
    }
}

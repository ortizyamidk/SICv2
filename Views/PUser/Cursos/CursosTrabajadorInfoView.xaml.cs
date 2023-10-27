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

        public CursosTrabajadorInfoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            


        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(); // Establece el enfoque en el TextBox
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

                TrabajadorRepository trabajadorRepository = new TrabajadorRepository();
                IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesById(idCurso);
                ObservableCollection<TrabajadorModel> participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                cursosTrabajadorDataGrid.ItemsSource = participantes;

                var repositoryAsistencia = new CursoGRepository();

                int cursoimp = repositoryAsistencia.CursoImpartido(txtCurso.Text);

                if(cursoimp == 1)
                {
                    border.Visibility = Visibility.Visible;

                }
                else
                {
                    border.Visibility = Visibility.Visible;

                    SolidColorBrush colorNOimpartido = new SolidColorBrush(Colors.Red);
                    border.Background = colorNOimpartido;
                    txtImpartido.Text = "NO IMPARTIDO";
                }

                txtSearch.Focus();
            }            
        }

        private void btnSig_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAnt_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

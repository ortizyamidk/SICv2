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
        public CursosTrabajadorInfoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            var repository = new CursoRepository();
            CursoGModel asistencia = repository.GetAsistenciaById(23); //traer idlista

            txtNoLista.Text = asistencia.Id.ToString();
            txtCurso.Text = asistencia.NomCurso.ToString();
            txtArea.Text = asistencia.AreaTematica.ToString();
            txtInicia.Text = asistencia.Inicia.ToString();
            txtTerm.Text = asistencia.Termina.ToString();
            txtHor.Text = asistencia.Horario.ToString();
            txtDur.Text = asistencia.Duracion.ToString() + " min";
            txtLugar.Text = asistencia.Lugar.ToString();
            txtInst.Text = asistencia.Instructor.ToString();

            int idlista = int.Parse(txtNoLista.Text);

            TrabajadorRepository trabajadorRepository = new TrabajadorRepository();
            IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesListaA(idlista);
            ObservableCollection<TrabajadorModel> participantes = new ObservableCollection<TrabajadorModel>(participantesList);
            cursosTrabajadorDataGrid.ItemsSource = participantes;

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
                //hacer consulta. Hacer otro if por si no existe el curso
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

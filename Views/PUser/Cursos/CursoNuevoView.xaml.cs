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

        CursoRepository cursoRepository;

        public CursoNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            cursoRepository = new CursoRepository();

            cbArea.SelectionChanged += ComboBox_SelectionChanged;
            txtcbArea.LostFocus += TextBox_LostFocus;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si el primer elemento está seleccionado (índice 0)
            if (cbArea.SelectedIndex == 0)
            {
                // Mostrar el TextBox y ocultar el ComboBox
                txtcbArea.Visibility = Visibility.Visible;
                cbArea.Visibility = Visibility.Collapsed;
                txtcbArea.Focus();
            }
            else
            {
                // Ocultar el TextBox y mostrar el ComboBox
                txtcbArea.Visibility = Visibility.Hidden;
                cbArea.Visibility = Visibility.Visible;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Verificar si el TextBox está vacío al perder el enfoque
            if (string.IsNullOrWhiteSpace(txtcbArea.Text))
            {
                // Mostrar el ComboBox y ocultar el TextBox
                txtcbArea.Visibility = Visibility.Hidden;
                cbArea.Visibility = Visibility.Visible;
                cbArea.SelectedIndex = 1;
            }
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombreC.Focus();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreC.Text))
            {
                errCurso.Content = req;
                txtNombreC.BorderBrush = bordeError;
            }
            else
            {
                ComboBoxItem messeleccionado = (ComboBoxItem) cbMes.SelectedItem;

                string nombrecurso = txtNombreC.Text;
                string meslimite = messeleccionado.Content.ToString();

                if (cbArea.SelectedIndex==0)
                {
                    string areaespecifica = txtcbArea.Text;
                    cursoRepository.AddCurso(nombrecurso, areaespecifica, meslimite);
                }
                else
                {
                    ComboBoxItem areaseleccionada = (ComboBoxItem) cbArea.SelectedItem;               
                    string areatematica = areaseleccionada.Content.ToString();
                    cursoRepository.AddCurso(nombrecurso, areatematica, meslimite);
                }                              
                
                MostrarCustomMessageBox();
                limpiar();
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }

        private void limpiar()
        {
            errCurso.Content = string.Empty;
            txtNombreC.BorderBrush = bordeNormal;
            txtNombreC.Text = string.Empty;
            txtNombreC.Focus();
            cbArea.SelectedIndex = 1;
            cbMes.SelectedIndex = 1;
        }
    }
}

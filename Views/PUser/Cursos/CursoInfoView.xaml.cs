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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para CursoInfoView.xaml
    /// </summary>
    public partial class CursoInfoView : UserControl
    {

        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public CursoInfoView()
        {
            InitializeComponent();

            deshabilitar();
        }

        public void habilitar()
        {
            txtNombreC.IsEnabled = true;
            cbAreaT.IsEnabled = true;
            cbMes.IsEnabled = true;

            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;

            txtNombreC.Focus();
        }

        public void deshabilitar()
        {
            txtNoCurso.IsEnabled = false;
            txtNombreC.IsEnabled = false;
            cbAreaT.IsEnabled = false;
            cbMes.IsEnabled = false;

            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            habilitar();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreC.Text))
            {
                errCurso.Content = req;
                txtNombreC.BorderBrush = bordeError;
            }
            else
            {
                MostrarCustomMessageBox();
                errCurso.Content = string.Empty;
                txtNombreC.BorderBrush = bordeNormal;
                deshabilitar();
            }           
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }       
    }
}

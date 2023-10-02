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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WPF_LoginForm.Views
{

    public partial class CursoNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public CursoNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

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
                MostrarCustomMessageBox();
                errCurso.Content = string.Empty;
                txtNombreC.BorderBrush = bordeNormal;
                txtNombreC.Text = string.Empty;
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }
    }
}

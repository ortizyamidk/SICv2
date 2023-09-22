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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para CursoInfoView.xaml
    /// </summary>
    public partial class CursoInfoView : UserControl
    {
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
            deshabilitar();
        }
    }
}

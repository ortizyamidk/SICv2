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
using static WPF_LoginForm.Views.CustomerView;

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Lógica de interacción para InstructorNuevoView.xaml
    /// </summary>
    public partial class InstructorNuevoView : UserControl
    {
        public InstructorNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombreI.Focus();
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.ThumbsUp;
            txtDescripcion.Text = "¡Registro guardado correctamente!";
            btnA.Content = "Aceptar";
        }
    }
}

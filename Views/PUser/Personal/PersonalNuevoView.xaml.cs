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
    /// Lógica de interacción para PersonalNuevoView.xaml
    /// </summary>
    public partial class PersonalNuevoView : UserControl
    {
        public PersonalNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            txtJefe.IsEnabled = false;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNoFicha.Focus();
        }

        

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

                string texto = txtNoFicha.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    MessageBox.Show("El campo no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    icono.Icon = FontAwesome.Sharp.IconChar.ThumbsUp;
                    txtDescripcion.Text = "¡Registro guardado correctamente!";
                    btnA.Content = "Aceptar";
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

        // Método para verificar si una cadena es numérica
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _); // Intenta convertir el texto a un entero
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
    }
}

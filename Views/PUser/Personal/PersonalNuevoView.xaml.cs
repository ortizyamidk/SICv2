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

    public partial class PersonalNuevoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public PersonalNuevoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            txtJefe.IsEnabled = false;

            dtIngreso.SelectedDate = DateTime.Now;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtNoFicha.Focus();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;

            txtNoFicha.BorderBrush = bordeNormal;
            txtNombre.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;

            if (string.IsNullOrEmpty(txtNoFicha.Text))
            {
                txtNoFicha.BorderBrush = bordeError;
                txtNoFicha.Focus();
                errores = true;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                errNombre.Content = req;
                txtNombre.BorderBrush = bordeError;
                errores = true;
            }

            if (string.IsNullOrEmpty(txtRFC.Text))
            {
                errRfc.Content = req;
                txtRFC.BorderBrush = bordeError;
                errores = true;
            }
            else if (txtRFC.Text.Length < 13)
            {
                errRfc.Content = "Al menos 13 caracteres";
                txtRFC.BorderBrush = bordeError;
                errores = true;
            }

            if (!errores)
            {               
                MostrarCustomMessageBox();
                limpiar();
            }

        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }

        public void limpiar()
        {
            txtNoFicha.Text = string.Empty;
            dtIngreso.SelectedDate = DateTime.Now;
            txtNombre.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtAntecedentes.Text = string.Empty;
            chkAudit.IsChecked = false;
            chkCalif.IsChecked = false;

            txtNoFicha.Focus();
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

        private void dtIngreso_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dtIngreso.SelectedDate;
            DateTime fechaActual = DateTime.Now;

            if (selectedDate.HasValue)
            {
                if (selectedDate > fechaActual)
                {
                    errFecha.Content = "No puede ser posterior a la actual";
                    errFecha.Visibility = Visibility.Visible;
                    btnGuardar.IsEnabled = false;

                }
                else
                {
                    errFecha.Visibility = Visibility.Collapsed;
                    btnGuardar.IsEnabled = true;
                }

                dtIngreso.BorderBrush = bordeNormal;
            }
            else
            {
                btnGuardar.IsEnabled = false;
                errFecha.Content = req;
                dtIngreso.FontSize = 13;
                errFecha.Visibility = Visibility.Visible;
                dtIngreso.BorderBrush = bordeError;
            }
        }

        private void btnLimp_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
    }
}

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
    public partial class InstructorInfoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public InstructorInfoView()
        {
            InitializeComponent();

            deshabilitar();
        }

        public void habilitar()
        {
            txtNombreI.IsEnabled = true;
            txtRFC.IsEnabled = true;
            cbTipo.IsEnabled = true;
            txtCompania.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;

            txtNombreI.Focus();
        }

        public void deshabilitar()
        {
            txtNoInst.IsEnabled = false;
            txtNombreI.IsEnabled = false;
            txtRFC.IsEnabled = false;
            cbTipo.IsEnabled = false;
            txtCompania.IsEnabled = false;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            habilitar();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;
            errComp.Content = string.Empty;

            txtNombreI.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;
            txtCompania.BorderBrush = bordeNormal;

            if (string.IsNullOrEmpty(txtNombreI.Text))
            {
                errNombre.Content = req;
                txtNombreI.BorderBrush = bordeError;
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
                errRfc.Content = "El RFC debe tener al menos 13 caracteres";
                txtRFC.BorderBrush = bordeError;
                errores = true;
            }

            if (string.IsNullOrEmpty(txtCompania.Text))
            {
                errComp.Content = req;
                txtCompania.BorderBrush = bordeError;
                errores = true;
            }

            if (!errores)
            {              
                MostrarCustomMessageBox();
                deshabilitar();
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
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

        private void txtRFC_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Validar que el carácter ingresado sea válido (letra o número).
            if (!char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            // Obtener el texto actual del TextBox.
            string currentText = txtRFC.Text;

            // Aplicar máscara de entrada.
            if (currentText.Length < 4)
            {
                // Permitir letras (primeras 4 letras).
                e.Handled = !char.IsLetter(e.Text, 0);
            }
            else if (currentText.Length < 10)
            {
                // Permitir números (6 números).
                e.Handled = !char.IsDigit(e.Text, 0);
            }
        }
    }
}

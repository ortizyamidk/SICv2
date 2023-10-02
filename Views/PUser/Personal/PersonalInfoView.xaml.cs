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
    
    public partial class PersonalInfoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public PersonalInfoView()
        {
            InitializeComponent();

            Deshabilitar();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Habilitar();   
        }

        public void Habilitar()
        {
            cbCateg.IsEnabled = true;
            cbNivel.IsEnabled = true;
            txtNombre.IsEnabled = true;
            cbPuesto.IsEnabled = true;
            cbArea.IsEnabled = true;
            chkAuditor.IsEnabled = true;
            chkCalif.IsEnabled = true;
            txtAntecedentes.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnEdit.IsEnabled = false;
            cbDpto.IsEnabled = true;
            txtRFC.IsEnabled = true;


            txtNombre.Focus();
        }

        public void Deshabilitar()
        {
            cbCateg.IsEnabled = false;
            cbNivel.IsEnabled = false;
            txtNombre.IsEnabled = false;
            cbPuesto.IsEnabled = false;
            cbArea.IsEnabled = false;
            cbDpto.IsEnabled = false;
            txtJefe.IsEnabled = false;
            chkAuditor.IsEnabled = false;
            chkCalif.IsEnabled = false;
            txtAntecedentes.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnEdit.IsEnabled = true;
            txtRFC.IsEnabled= false;
            txtNoTarj.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool errores = false;

            // Restablecer los mensajes de error y los bordes al estado inicial
            errNombre.Content = string.Empty;
            errRfc.Content = string.Empty;

            txtNombre.BorderBrush = bordeNormal;
            txtRFC.BorderBrush = bordeNormal;

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
                Deshabilitar();               
            }
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxCustom customMessageBox = new MessageBoxCustom();
            customMessageBox.ShowDialog();
        }

        private void btnSig_Click(object sender, RoutedEventArgs e)
        {
            
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

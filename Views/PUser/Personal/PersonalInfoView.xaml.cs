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
    /// Lógica de interacción para PersonalInfoView.xaml
    /// </summary>
    public partial class PersonalInfoView : UserControl
    {
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
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            icono.Icon = FontAwesome.Sharp.IconChar.ThumbsUp;
            txtDescripcion.Text = "¡Registro editado correctamente!";
            btnA.Content = "Aceptar";
            Deshabilitar();
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

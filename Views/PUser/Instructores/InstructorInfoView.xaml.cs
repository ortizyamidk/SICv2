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
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views
{
    public partial class InstructorInfoView : UserControl
    {
        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        InstructorRepository repository;
        string nombre, rfc, tipo, comp;
        int id;

        public InstructorInfoView()
        {
            InitializeComponent();
            deshabilitar();
            

            repository = new InstructorRepository();
            InstructorModel instructor = (repository as IInstructorRepository).GetById(5); //traer id del instructor seleccionado en la tabla

            if(instructor != null)
            {
                txtNoInst.Text = instructor.Id.ToString();
                txtNombreI.Text = instructor.NomInstr.ToString();
                txtRFC.Text = instructor.RFC.ToString();
                txtCompania.Text=instructor.NomCia.ToString();

                if (instructor.TipoInstr.Equals("Interno"))
                {
                    cbTipo.SelectedIndex = 0;
                }
                if (instructor.TipoInstr.Equals("Externo"))
                {
                    cbTipo.SelectedIndex = 1;
                }
            }
            else
            {
                // Maneja el caso en el que el instructor es null
                MessageBox.Show("Instructor no encontrado", "Error");

                txtNoInst.Text = string.Empty; 
                txtNombreI.Text = string.Empty;
                txtRFC.Text = string.Empty;
                txtCompania.Text = string.Empty;
                cbTipo.SelectedIndex = 0;
            }
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
                id = int.Parse(txtNoInst.Text);
                nombre = txtNombreI.Text;
                rfc = txtRFC.Text;
                ComboBoxItem instructorS = (ComboBoxItem)cbTipo.SelectedItem;
                tipo = instructorS.Content.ToString();
                comp = txtCompania.Text;


                repository.EditInstructor(nombre, rfc, tipo, comp, id);
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

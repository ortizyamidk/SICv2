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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WPF_LoginForm.Views
{
    public partial class CursoInfoView : UserControl
    {

        SolidColorBrush bordeError = new SolidColorBrush(Colors.Red);
        SolidColorBrush bordeNormal = new SolidColorBrush(Colors.Black);
        string req = "*Campo requerido";

        public CursoInfoView()
        {
            InitializeComponent();

            deshabilitar();

            var cursoRepository = new CursoRepository();
            CursoModel curso = cursoRepository.GetById(9);

            txtNoCurso.Text = curso.Id.ToString();
            txtNombreC.Text = curso.NomCurso.ToString();

            string areaTematica = curso.AreaTematica.ToString(); // Obtén el valor de la base de datos

            foreach (ComboBoxItem item in cbAreaT.Items)
            {
                if (item.Content.ToString() == areaTematica)
                {
                    cbAreaT.SelectedItem = item; // Selecciona el elemento coincidente
                    break; // Termina el bucle una vez que se encuentra la coincidencia
                }
            }

            string mesLimite = curso.MesLimite.ToString();
            foreach (ComboBoxItem itemMes in cbMes.Items)
            {
                if (itemMes.Content.ToString() == mesLimite)
                {
                    cbMes.SelectedItem = itemMes; // Selecciona el elemento coincidente
                    break; // Termina el bucle una vez que se encuentra la coincidencia
                }
            }


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
                ComboBoxItem areaseleccionada = (ComboBoxItem)cbAreaT.SelectedItem;
                ComboBoxItem messeleccionado = (ComboBoxItem)cbMes.SelectedItem;

                int idcurso = int.Parse(txtNoCurso.Text);
                string nombrecurso = txtNombreC.Text;
                string areatematica = areaseleccionada.Content.ToString();
                string meslimite = messeleccionado.Content.ToString();

                var cursoEdit = new CursoRepository();
                cursoEdit.EditCurso(nombrecurso, areatematica, meslimite, idcurso);

                
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

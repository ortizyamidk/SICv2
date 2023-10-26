using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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


namespace WPF_LoginForm.Views.GUser
{
    public partial class CursoInfoGView : UserControl
    {

        public CursoInfoGView()
        {
            InitializeComponent();

            var repository = new CursoGRepository();
            CursoGModel asistencia = repository.GetById(81); //traer idlistaasistencia seleccionada en la tabla de CursoGView

            txtNoLista.Text = asistencia.Id.ToString();
            txtCurso.Text=asistencia.NomCurso.ToString();
            txtAreaT.Text=asistencia.AreaTematica.ToString();
            txtInicia.Text=asistencia.Inicia.ToString();
            txtTermina.Text=asistencia.Termina.ToString();
            txtHorario.Text=asistencia.Horario.ToString();
            txtDura.Text=asistencia.Duracion.ToString() + " min";
            txtLugar.Text=asistencia.Lugar.ToString();
            txtInst.Text=asistencia.Instructor.ToString();

            int idlistacurso = int.Parse(txtNoLista.Text);

            TrabajadorRepository trabajadorRepository = new TrabajadorRepository();
            IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesListaA(idlistacurso);
            ObservableCollection<TrabajadorModel> participantes = new ObservableCollection<TrabajadorModel>(participantesList);
            listaDataGrid.ItemsSource = participantes;
        }


        private void btnPaseLista_Click(object sender, RoutedEventArgs e)
        {
            MostrarCustomMessageBox();
        }

        private void MostrarCustomMessageBox()
        {
            MessageBoxPaseLista customMessageBox = new MessageBoxPaseLista();
            customMessageBox.ShowDialog();
        }
    }
}

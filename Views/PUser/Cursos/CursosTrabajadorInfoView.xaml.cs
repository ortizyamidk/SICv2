using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{

    public partial class CursosTrabajadorInfoView : UserControl
    {
        string idCurso;

        ObservableCollection<TrabajadorModel> participantes;
        
        DepartamentoRepository departamentoRepository;
        TrabajadorRepository trabajadorRepository;
        CursoGRepository cursoGRepository;
        AreaRepository areaRepository;

        public CursosTrabajadorInfoView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            departamentoRepository = new DepartamentoRepository();
            trabajadorRepository = new TrabajadorRepository();
            cursoGRepository = new CursoGRepository();
            areaRepository = new AreaRepository();
        }

        private void CargarAreas()
        {
            try
            {
                var areas = areaRepository.GetByAll();
                foreach (var area in areas)
                {
                    var item = new ComboBoxItem
                    {
                        Content = area.NombreArea
                    };
                    cbArea.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }                  
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
          
            CargarAreas();
            btnPaseLista.Visibility = Visibility.Collapsed;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    MessageBox.Show("Ingrese un No. de curso", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    txtSearch.Focus();
                }
                else
                {
                    var repository = new CursoRepository();
                    CursoGModel asistencia = repository.GetAsistenciaById(txtSearch.Text);

                    // Restaurar la visibilidad y el estado inicial
                    border.Visibility = Visibility.Collapsed;
                    txtImpartido.Visibility = Visibility.Collapsed;
                    txtImpartido.Text = string.Empty;

                    if (asistencia != null)
                    {                   
                        cbArea.SelectedIndex = 0;

                        txtIDCurso.Text = asistencia.IdCurso.ToString();
                        txtCurso.Text = asistencia.NomCurso.ToString();
                        txtArea.Text = asistencia.AreaTematica.ToString();
                        txtInicia.Text = asistencia.Inicia.ToString();
                        txtTerm.Text = asistencia.Termina.ToString();
                        txtHor.Text = asistencia.Horario.ToString();
                        txtDur.Text = asistencia.Duracion.ToString() + " min";
                        txtLugar.Text = asistencia.Lugar.ToString();
                        txtInst.Text = asistencia.Instructor.ToString();

                        idCurso = txtIDCurso.Text;

                        IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesById(idCurso);
                        participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                        cursosTrabajadorDataGrid.ItemsSource = participantes;

                        cbBorder.Visibility = Visibility.Visible;

                        int cursoimp = cursoGRepository.CursoImpartido(txtCurso.Text);

                        if (cursoimp == 1)
                        {
                            border.Visibility = Visibility.Visible;
                            txtImpartido.Visibility = Visibility.Visible;

                            SolidColorBrush colorImpartido = new SolidColorBrush(Colors.Green);
                            border.Background = colorImpartido;
                            txtImpartido.Text = "IMPARTIDO";
                            btnPaseLista.Visibility = Visibility.Collapsed;
                        }
                        else if (cursoimp == 0)
                        {
                            border.Visibility = Visibility.Visible;
                            txtImpartido.Visibility = Visibility.Visible;

                            SolidColorBrush colorNOimpartido = new SolidColorBrush(Colors.Red);
                            border.Background = colorNOimpartido;
                            txtImpartido.Text = "NO IMPARTIDO";
                            btnPaseLista.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            border.Visibility = Visibility.Collapsed;
                            txtImpartido.Visibility = Visibility.Collapsed;
                            txtImpartido.Text = string.Empty;
                            btnPaseLista.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        Limpiar();
                        MessageBox.Show("No existe curso con ese ID o no se ha registrado la Lista", "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                
                    txtSearch.Focus();
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                      
        }

        private void Limpiar()
        {
            
            if (participantes != null)
            {
                txtIDCurso.Text = string.Empty;
                txtCurso.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtInicia.Text = string.Empty;
                txtTerm.Text = string.Empty;
                txtHor.Text = string.Empty;
                txtDur.Text = string.Empty;
                txtLugar.Text = string.Empty;
                txtInst.Text = string.Empty;
                txtSearch.Text = string.Empty;

                border.Visibility = Visibility.Collapsed;
                cbBorder.Visibility = Visibility.Collapsed;
                

                participantes.Clear();
            }
            else
            {
                txtIDCurso.Text = string.Empty;
                txtCurso.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtInicia.Text = string.Empty;
                txtTerm.Text = string.Empty;
                txtHor.Text = string.Empty;
                txtDur.Text = string.Empty;
                txtLugar.Text = string.Empty;
                txtInst.Text = string.Empty;
                txtSearch.Text = string.Empty;

                border.Visibility = Visibility.Collapsed;
                cbBorder.Visibility = Visibility.Collapsed;
            }

            btnPaseLista.Visibility = Visibility.Collapsed;                    
        }

        private void CursosTrabajadorInfoView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbArea.SelectedIndex > 1)
                {               
                    ComboBoxItem areaseleccionada = (ComboBoxItem)cbArea.SelectedItem;
                    string area = areaseleccionada.Content.ToString();

                    IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesByIdAndArea(idCurso, area);
                    participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                    cursosTrabajadorDataGrid.ItemsSource = participantes;
                }
                else if(cbArea.SelectedIndex == 1)
                {
                    IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesById(idCurso);
                    participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                    cursosTrabajadorDataGrid.ItemsSource = participantes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }                      
        }

        private void btnPaseLista_Click(object sender, RoutedEventArgs e)
        {
            MostrarCustomMessageBox();
        }

        private void MostrarCustomMessageBox()
        {
            string idcurso = txtIDCurso.Text;
            MessageBoxPaseLista customMessageBox = new MessageBoxPaseLista(idcurso);
            customMessageBox.ShowDialog();
        }
    }
}

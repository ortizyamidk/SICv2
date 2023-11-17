using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;


namespace WPF_LoginForm.Views.GUser
{
    public partial class CursoInfoGView : UserControl
    {
        CursoGRepository cursoGRepository;

        SolidColorBrush colorImpartido;
        SolidColorBrush colorNOimpartido;

        ObservableCollection<TrabajadorModel> participantes;

        public CursoInfoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            colorImpartido = new SolidColorBrush(Colors.Green);
            colorNOimpartido = new SolidColorBrush(Colors.Red);

            cursoGRepository = new CursoGRepository();                    
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    MessageBox.Show("Ingrese un No. de lista", "Campo vacío", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Limpiar();
                }
                else
                {
                    int idlista = int.Parse(txtSearch.Text);                   

                    TrabajadorRepository trabajadorRepository = new TrabajadorRepository();
                    var viewModel = (CursoInfoGViewModel)DataContext;
                    string arealoggeada = viewModel.CurrentUserAccount.DisplayArea;

                    CursoGModel asistencia = cursoGRepository.GetById(idlista, arealoggeada);

                    IEnumerable<TrabajadorModel> participantesList = trabajadorRepository.GetParticipantesListaA(idlista, arealoggeada);
                    participantes = new ObservableCollection<TrabajadorModel>(participantesList);
                    listaDataGrid.ItemsSource = participantes;

                    if (asistencia != null && participantes.Count > 0)
                    {
                        txtNoLista.Text = asistencia.IdLista.ToString();
                        txtIDCurso.Text = asistencia.IdCurso.ToString();
                        txtCurso.Text = asistencia.NomCurso.ToString();
                        txtAreaT.Text = asistencia.AreaTematica.ToString();
                        txtInicia.Text = asistencia.Inicia.ToString();
                        txtTermina.Text = asistencia.Termina.ToString();
                        txtHorario.Text = asistencia.Horario.ToString();
                        txtDura.Text = asistencia.Duracion.ToString() + " min";
                        txtLugar.Text = asistencia.Lugar.ToString();
                        txtInst.Text = asistencia.Instructor.ToString();

                        int cursoimp = cursoGRepository.CursoImpartido(txtCurso.Text);

                        if (cursoimp == 1)
                        {
                            border.Visibility = Visibility.Visible;
                            txtImpartido.Visibility = Visibility.Visible;

                            border.Background = colorImpartido;
                            txtImpartido.Text = "IMPARTIDO";
                        }
                        else if (cursoimp == 0)
                        {
                            border.Visibility = Visibility.Visible;
                            txtImpartido.Visibility = Visibility.Visible;


                            border.Background = colorNOimpartido;
                            txtImpartido.Text = "NO IMPARTIDO";
                        }
                        else
                        {
                            border.Visibility = Visibility.Collapsed;
                            txtImpartido.Visibility = Visibility.Collapsed;
                            txtImpartido.Text = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existe lista de asistencia con ese ID o no pertenece al área de: " + arealoggeada, "Inválido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Limpiar();
                    }
                }

            }
            catch (Exception ex)
            {
                // Manejo de la excepción: puedes registrar el error, notificar al usuario, etc.
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limpiar()
        {
            txtSearch.Text = string.Empty;
            txtIDCurso.Text = string.Empty;
            txtNoLista.Text = string.Empty;
            txtCurso.Text = string.Empty;
            txtAreaT.Text = string.Empty;
            txtLugar.Text = string.Empty;
            txtInst.Text = string.Empty;
            txtInicia.Text = string.Empty;
            txtTermina.Text = string.Empty;
            txtHorario.Text = string.Empty;
            txtDura.Text = string.Empty;

            border.Visibility = Visibility.Collapsed;
            txtImpartido.Visibility = Visibility.Collapsed;
            txtImpartido.Text = string.Empty;

            if (participantes!=null)
            {
                participantes.Clear();
            }

            txtSearch.Focus();
            
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch_Click(sender, e);
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
    }
}

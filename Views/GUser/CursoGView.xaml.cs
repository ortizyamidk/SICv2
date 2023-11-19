using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;

namespace WPF_LoginForm.Views.GUser
{

    public partial class CursoGView : UserControl
    {
        private ObservableCollection<CursoGModel> cursos;
        private ICollectionView listaFiltrada;
        CursoGRepository repository;

        public CursoGView()
        {
            InitializeComponent();
            repository = new CursoGRepository();

            Loaded += MainWindow_Loaded;                       
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();

            var viewModel = (CursoGViewModel)DataContext;

                try
                {
                    IEnumerable<CursoGModel> cursosList = repository.GetByAll(viewModel.CurrentUserAccount.DisplayArea); //obtener area loggeada
                    cursos = new ObservableCollection<CursoGModel>(cursosList);
                    cursosGDataGrid.ItemsSource = cursos;

                    //filtrar busqueda
                    listaFiltrada = CollectionViewSource.GetDefaultView(cursos);
                    cursosGDataGrid.ItemsSource = listaFiltrada;
                    txtSearch.TextChanged += TxtSearch_TextChanged;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los cursos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        //filtrar
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower(); // Convierte el texto a minúsculas

            if (string.IsNullOrEmpty(search))
            {
                listaFiltrada.Filter = null; // Si el texto está vacío, muestra todos los elementos
            }
            else
            {
                listaFiltrada.Filter = item =>
                {
                    var curso = item as CursoGModel;
                    return curso.NomCurso.ToLower().Contains(search);
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.Views
{

    public partial class CursosView : UserControl
    {

        //filtrar
        private ObservableCollection<CursoModel> cursoOriginal;
        private ICollectionView cursoFiltrado;

        public CursosView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            CursoRepository repository = new CursoRepository();
            try
            {
                IEnumerable<CursoModel> cursoList = repository.GetByAll();        
                ObservableCollection<CursoModel> cursos = new ObservableCollection<CursoModel>(cursoList);
                cursosDataGrid.ItemsSource = cursos;

                //filtrar
                cursoOriginal = cursos;
                cursoFiltrado = CollectionViewSource.GetDefaultView(cursoOriginal);
                cursosDataGrid.ItemsSource = cursoFiltrado;
                txtSearch.TextChanged += TxtSearch_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(); 
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

        //filtrar
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower(); // Convierte el texto a minúsculas

            if (string.IsNullOrEmpty(search))
            {
                cursoFiltrado.Filter = null; // Si el texto está vacío, muestra todos los elementos
            }
            else
            {
                cursoFiltrado.Filter = item =>
                {
                    var cursoBuscado = item as CursoModel;
                    return cursoBuscado.NomCurso.ToLower().Contains(search);
                };
            }
        }
    }
}

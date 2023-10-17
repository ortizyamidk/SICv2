using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;
using static WPF_LoginForm.Views.GUser.CursoNuevoGView;

namespace WPF_LoginForm.Views.GUser
{

    public partial class CursoGView : UserControl
    {
        //filtrar
        private ObservableCollection<CursoGModel> listaOriginal;
        private ICollectionView listaFiltrada;

        public CursoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            CursoGRepository repository = new CursoGRepository();
            IEnumerable<CursoGModel> cursosList = repository.GetByAll();
            ObservableCollection<CursoGModel> cursos = new ObservableCollection<CursoGModel>(cursosList);
            cursosGDataGrid.ItemsSource = cursos;

            //filtrar busqueda
            listaOriginal = cursos;
            listaFiltrada = CollectionViewSource.GetDefaultView(listaOriginal);
            cursosGDataGrid.ItemsSource = listaFiltrada;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            
        }

        //enfocar en barra de busqueda
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
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

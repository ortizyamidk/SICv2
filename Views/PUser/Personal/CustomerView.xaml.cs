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

namespace WPF_LoginForm.Views
{

    public partial class CustomerView : UserControl
    {
        //filtrar
        private ObservableCollection<Personal> original;
        private ICollectionView filtrado;

        public CustomerView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            var converter = new BrushConverter();
            ObservableCollection<Personal> trabajadores = new ObservableCollection<Personal>();

            trabajadores.Add(new Personal { numficha = "57019", nombre = "Perez Arredondo Manuel Eduardo", area = "Almacen de componentes", puesto = "Acomodo de material" });
            trabajadores.Add(new Personal { numficha = "2", nombre = "Trabajador 2", area = "Área 2", puesto = "Puesto 2" });
            trabajadores.Add(new Personal { numficha = "3", nombre = "Trabajador 3", area = "Área 3", puesto = "Puesto 3" });
            trabajadores.Add(new Personal { numficha = "4", nombre = "Trabajador 4", area = "Área 4", puesto = "Puesto 4" });
            trabajadores.Add(new Personal { numficha = "5", nombre = "Trabajador 5", area = "Área 5", puesto = "Puesto 5" });
            trabajadores.Add(new Personal { numficha = "6", nombre = "Trabajador 6", area = "Área 6", puesto = "Puesto 6" });
            trabajadores.Add(new Personal { numficha = "7", nombre = "Trabajador 7", area = "Área 7", puesto = "Puesto 7" });
            trabajadores.Add(new Personal { numficha = "8", nombre = "Trabajador 8", area = "Área 8", puesto = "Puesto 8" });
            trabajadores.Add(new Personal { numficha = "9", nombre = "Trabajador 9", area = "Área 9", puesto = "Puesto 9" });
            trabajadores.Add(new Personal { numficha = "10", nombre = "Trabajador 10", area = "Área 10", puesto = "Puesto 10" });

            personalDataGrid.ItemsSource = trabajadores;

            //filtrar
            original = trabajadores;
            filtrado = CollectionViewSource.GetDefaultView(original);
            personalDataGrid.ItemsSource = filtrado;
            txtSearch.TextChanged += TxtSearch_TextChanged;

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        public class Personal
        {
            public string numficha { get; set; }
            public string nombre { get; set; }
            public string area { get; set; }
            public string puesto { get; set; }
 
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus(); // Establece el enfoque en el TextBox
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
                filtrado.Filter = null; // Si el texto está vacío, muestra todos los elementos
            }
            else
            {
                filtrado.Filter = item =>
                {
                    var curso = item as Personal;
                    return curso.nombre.ToLower().Contains(search);
                };
            }
        }

        private void personalDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (personalDataGrid.SelectedItem != null)
            {
                // Obtén el valor de la columna "#" (num)
                string numValue = (personalDataGrid.SelectedItem as Personal)?.numficha;

                // Ahora, puedes usar la variable 'numValue' para hacer lo que necesites con ese valor.
                //txtSearch.Text = numValue.ToString();
            }
        }
    }
}

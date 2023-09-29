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
    public partial class InstructoresView : UserControl
    {

        //filtrar
        private ObservableCollection<Instructor> original;
        private ICollectionView filtrado;

        public InstructoresView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            var converter = new BrushConverter();
            ObservableCollection<Instructor> instructores = new ObservableCollection<Instructor>();

            instructores.Add(new Instructor { num = "50156", nombre = "Jose Dominguez", tipo = "Interno" });
            instructores.Add(new Instructor { num = "2", nombre = "Instructor 2", tipo = "Externo" });
            instructores.Add(new Instructor { num = "3", nombre = "Instructor 3", tipo = "Externo" });
            instructores.Add(new Instructor { num = "4", nombre = "Instructor 4", tipo = "Interno" });
            instructores.Add(new Instructor { num = "5", nombre = "Instructor 5", tipo = "Externo" });
            instructores.Add(new Instructor { num = "6", nombre = "Instructor 6", tipo = "Interno" });
            instructores.Add(new Instructor { num = "7", nombre = "Instructor 7", tipo = "Interno" });
            instructores.Add(new Instructor { num = "8", nombre = "Instructor 8", tipo = "Interno" });
            instructores.Add(new Instructor { num = "9", nombre = "Instructor 9", tipo = "Interno" });
            instructores.Add(new Instructor { num = "10", nombre = "Instructor 10", tipo = "Externo" });

            instructoresDataGrid.ItemsSource = instructores;

            //filtrar
            original = instructores;
            filtrado = CollectionViewSource.GetDefaultView(original);
            instructoresDataGrid.ItemsSource = filtrado;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        public class Instructor
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string tipo { get; set; }

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
                    var curso = item as Instructor;
                    return curso.nombre.ToLower().Contains(search);
                };
            }
        }

        private void instructoresDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (instructoresDataGrid.SelectedItem != null)
            {
                // Obtén el valor de la columna "#" (num)
                string numValue = (instructoresDataGrid.SelectedItem as Instructor)?.num;

                // Ahora, puedes usar la variable 'numValue' para hacer lo que necesites con ese valor.
                //txtSearch.Text = numValue.ToString();
            }
        }
    }
}

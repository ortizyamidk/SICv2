using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Lógica de interacción para InstructoresView.xaml
    /// </summary>
    public partial class InstructoresView : UserControl
    {
        public InstructoresView()
        {
            InitializeComponent();
            txtSearch.Focus();

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

            instructores.Add(new Instructor { num = "1", nombre = "Instructor 1", tipo = "Interno" });
            instructores.Add(new Instructor { num = "2", nombre = "Instructor 2", tipo = "Externo" });
            instructores.Add(new Instructor { num = "3", nombre = "Instructor 3", tipo = "Externo" });
            instructores.Add(new Instructor { num = "4", nombre = "Instructor 4", tipo = "Interno" });
            instructores.Add(new Instructor { num = "5", nombre = "Instructor 5", tipo = "Externo" });
            instructores.Add(new Instructor { num = "6", nombre = "Instructor 6", tipo = "Interno" });
            instructores.Add(new Instructor { num = "7", nombre = "Instructor 7", tipo = "Interno" });
            instructores.Add(new Instructor { num = "8", nombre = "Instructor 8", tipo = "Interno" });
            instructores.Add(new Instructor { num = "9", nombre = "Instructor 9", tipo = "Interno" });
            instructores.Add(new Instructor { num = "10", nombre = "Instructor 10", tipo = "Externo" });

            instructores.Add(new Instructor { num = "1", nombre = "Instructor 1", tipo = "Interno" });
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
        }

        public class Instructor
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string tipo { get; set; }

        }
    }
}

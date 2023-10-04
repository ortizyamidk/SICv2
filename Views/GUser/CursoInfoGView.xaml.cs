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
using WPF_LoginForm.CustomControls;
using WPF_LoginForm.ViewModels;


namespace WPF_LoginForm.Views.GUser
{
    /// <summary>
    /// Lógica de interacción para CursoInfoGView.xaml
    /// </summary>
    public partial class CursoInfoGView : UserControl
    {

        public CursoInfoGView()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            ObservableCollection<Lista> lista = new ObservableCollection<Lista>();

            lista.Add(new Lista { num = "57019", nombre = "Perez Arredondo Manuel Eduardo", puesto = "Acomodo de material" });
            lista.Add(new Lista { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            lista.Add(new Lista { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            lista.Add(new Lista { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            lista.Add(new Lista { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            lista.Add(new Lista { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            lista.Add(new Lista { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            lista.Add(new Lista { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            lista.Add(new Lista { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            lista.Add(new Lista { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            listaDataGrid.ItemsSource = lista;

        }

        public class Lista
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
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

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
using static WPF_LoginForm.Views.GUser.CursoGView;

namespace WPF_LoginForm.Views.GUser
{
    /// <summary>
    /// Lógica de interacción para CursoNuevoGView.xaml
    /// </summary>
    public partial class CursoNuevoGView : UserControl
    {
        public CursoNuevoGView()
        {
            InitializeComponent();
            Loaded += CursoNuevoGView_Loaded;

            var converter = new BrushConverter();

        }

        private void CursoNuevoGView_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Participante> participantes = new ObservableCollection<Participante>();

            participantes.Add(new Participante { num = "1", nombre = "Trabajador 1", puesto = "Puesto 1" });
            participantes.Add(new Participante { num = "2", nombre = "Trabajador 2", puesto = "Puesto 2" });
            participantes.Add(new Participante { num = "3", nombre = "Trabajador 3", puesto = "Puesto 3" });
            participantes.Add(new Participante { num = "4", nombre = "Trabajador 4", puesto = "Puesto 4" });
            participantes.Add(new Participante { num = "5", nombre = "Trabajador 5", puesto = "Puesto 5" });
            participantes.Add(new Participante { num = "6", nombre = "Trabajador 6", puesto = "Puesto 6" });
            participantes.Add(new Participante { num = "7", nombre = "Trabajador 7", puesto = "Puesto 7" });
            participantes.Add(new Participante { num = "8", nombre = "Trabajador 8", puesto = "Puesto 8" });
            participantes.Add(new Participante { num = "9", nombre = "Trabajador 9", puesto = "Puesto 9" });
            participantes.Add(new Participante { num = "10", nombre = "Trabajador 10", puesto = "Puesto 10" });

            participantesDataGrid.ItemsSource = participantes;
        }

        public class Participante
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
        }
    }
}

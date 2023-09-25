using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para PersonalNuevoView.xaml
    /// </summary>
    public partial class PersonalNuevoView : UserControl
    {
        public PersonalNuevoView()
        {
            InitializeComponent();
            txtNoFicha.Focus();
            txtJefe.IsEnabled = false;
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace WPF_LoginForm.CustomControls
{
    public partial class cursosVencidos : UserControl
    {
        public cursosVencidos()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(cursosVencidos));

        public string Desc
        {
            get { return (string)GetValue(DescProperty); }
            set { SetValue(DescProperty, value); }
        }
        public static readonly DependencyProperty DescProperty = DependencyProperty.Register("Desc", typeof(string), typeof(cursosVencidos));

    }
}

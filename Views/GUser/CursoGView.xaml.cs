﻿using System;
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

namespace WPF_LoginForm.Views.GUser
{

    public partial class CursoGView : UserControl
    {
        
        public CursoGView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;


            var converter = new BrushConverter();
            ObservableCollection<ListaAsistencia> lista = new ObservableCollection<ListaAsistencia>();

            lista.Add(new ListaAsistencia { num = "128", nombre = "Bloqueo y Etiquetado", area = "Calidad", instructor = "Javier Aviles Ortiz", inicia = "12/10/2023", termina = "12/10/23" });
            lista.Add(new ListaAsistencia { num = "2", nombre = "Curso 2", area = "Area 2", instructor = "Instructor 2", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "3", nombre = "Curso 3", area = "Area 3", instructor = "Instructor 3", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "4", nombre = "Curso 4", area = "Area 4", instructor = "Instructor 4", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "5", nombre = "Curso 5", area = "Area 5", instructor = "Instructor 5", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "6", nombre = "Curso 6", area = "Area 6", instructor = "Instructor 6", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "7", nombre = "Curso 7", area = "Area 7", instructor = "Instructor 7", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "8", nombre = "Curso 8", area = "Area 8", instructor = "Instructor 8", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "9", nombre = "Curso 9", area = "Area 9", instructor = "Instructor 9", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "10", nombre = "Curso 10", area = "Area 10", instructor = "Instructor 10", inicia = "20/09/2023", termina = "21/09/23" });

            lista.Add(new ListaAsistencia { num = "1", nombre = "Curso 1", area = "Area 1", instructor = "Instructor 1", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "2", nombre = "Curso 2", area = "Area 2", instructor = "Instructor 2", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "3", nombre = "Curso 3", area = "Area 3", instructor = "Instructor 3", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "4", nombre = "Curso 4", area = "Area 4", instructor = "Instructor 4", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "5", nombre = "Curso 5", area = "Area 5", instructor = "Instructor 5", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "6", nombre = "Curso 6", area = "Area 6", instructor = "Instructor 6", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "7", nombre = "Curso 7", area = "Area 7", instructor = "Instructor 7", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "8", nombre = "Curso 8", area = "Area 8", instructor = "Instructor 8", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "9", nombre = "Curso 9", area = "Area 9", instructor = "Instructor 9", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "10", nombre = "Curso 10", area = "Area 10", instructor = "Instructor 10", inicia = "20/09/2023", termina = "21/09/23" });

            lista.Add(new ListaAsistencia { num = "1", nombre = "Curso 1", area = "Area 1", instructor = "Instructor 1", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "2", nombre = "Curso 2", area = "Area 2", instructor = "Instructor 2", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "3", nombre = "Curso 3", area = "Area 3", instructor = "Instructor 3", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "4", nombre = "Curso 4", area = "Area 4", instructor = "Instructor 4", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "5", nombre = "Curso 5", area = "Area 5", instructor = "Instructor 5", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "6", nombre = "Curso 6", area = "Area 6", instructor = "Instructor 6", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "7", nombre = "Curso 7", area = "Area 7", instructor = "Instructor 7", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "8", nombre = "Curso 8", area = "Area 8", instructor = "Instructor 8", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "9", nombre = "Curso 9", area = "Area 9", instructor = "Instructor 9", inicia = "20/09/2023", termina = "21/09/23" });
            lista.Add(new ListaAsistencia { num = "10", nombre = "Curso 10", area = "Area 10", instructor = "Instructor 10", inicia = "20/09/2023", termina = "21/09/23" });

            listaDataGrid.ItemsSource = lista;
        }

        public class ListaAsistencia
        {
            public string num { get; set; }
            public string nombre { get; set; }
            public string area { get; set; }
            public string instructor { get; set; }
            public string inicia { get; set; }
            public string termina { get; set; }

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void TextBox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado contiene solo letras
            if (!IsLetter(e.Text))
            {
                e.Handled = true; // Evita que se ingrese el carácter no alfabético
            }
        }

        // Método para verificar si una cadena contiene solo letras
        private bool IsLetter(string text)
        {
            return text.All(char.IsLetter);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("El campo no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {

            }
        }
    }
}

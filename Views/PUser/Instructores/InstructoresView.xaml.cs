﻿using System;
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
    public partial class InstructoresView : UserControl
    {
        //filtrar
        private ObservableCollection<InstructorModel> instructores;
        private ICollectionView filtrado;

        public InstructoresView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            // Crear una instancia de IntructorRepository
            InstructorRepository repository = new InstructorRepository();

            try
            {
                IEnumerable<InstructorModel> instructoresList = repository.GetByAll();
                ObservableCollection<InstructorModel> instructores = new ObservableCollection<InstructorModel>(instructoresList);

                // Asignar la lista de instructores al DataGrid
                instructoresDataGrid.ItemsSource = instructores;

                //filtrar
                filtrado = CollectionViewSource.GetDefaultView(instructores);
                instructoresDataGrid.ItemsSource = filtrado;
                txtSearch.TextChanged += TxtSearch_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
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
                    var instructor = item as InstructorModel;
                    return instructor.NomInstr.ToLower().Contains(search);
                };
            }
        }
    }
}

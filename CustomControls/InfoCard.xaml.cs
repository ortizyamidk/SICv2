﻿using System.Windows;
using System.Windows.Controls;

namespace WPF_LoginForm.CustomControls
{
    public partial class InfoCard : UserControl
    {
        public InfoCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(InfoCard));

        public string CursosRegistrados
        {
            get { return (string)GetValue(CursosRegistradosProperty); }
            set { SetValue(CursosRegistradosProperty, value); }
        }

        public static readonly DependencyProperty CursosRegistradosProperty = DependencyProperty.Register("CursosRegistrados", typeof(string), typeof(InfoCard));
        
        public string CursosARegistrar
        {
            get { return (string)GetValue(RegistrarProperty); }
            set { SetValue(RegistrarProperty, value); }
        }

        public static readonly DependencyProperty RegistrarProperty = DependencyProperty.Register("CursosARegistrar", typeof(string), typeof(InfoCard));

        public string Percentage
        {
            get { return (string)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public static readonly DependencyProperty PercentageProperty = DependencyProperty.Register("Percentage", typeof(string), typeof(InfoCard));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(InfoCard));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(InfoCard));

        public MahApps.Metro.IconPacks.PackIconMaterialKind Icon
        {
            get { return (MahApps.Metro.IconPacks.PackIconMaterialKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(MahApps.Metro.IconPacks.PackIconMaterialKind), typeof(InfoCard));
    }
}

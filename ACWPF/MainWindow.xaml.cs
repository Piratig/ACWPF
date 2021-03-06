﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


namespace ACWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InventaryCheck_Click(object sender, RoutedEventArgs e)
        {
            ((MainVM)DataContext).CheckInventary();
        }

        private void Extarder_Click(object sender, RoutedEventArgs e)
        {
            ((MainVM)DataContext).AddRegister("Выдано");
        }

        private void OrderFromStock_Click(object sender, RoutedEventArgs e)
        {
            ((MainVM)DataContext).AddRegister("Заказано со склада");
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            ((MainVM)DataContext).AddRegister("Резерв");
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            ((MainVM)DataContext).AddRegister("Закупка");
        }

        private void OpenRequest_Click(object sender, RoutedEventArgs e)
        {
            ((MainVM)DataContext).OpenRequest();
        }
    }
}

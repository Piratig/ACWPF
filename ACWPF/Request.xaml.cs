﻿using System;
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
using System.Windows.Shapes;

namespace ACWPF
{
    /// <summary>
    /// Логика взаимодействия для Request.xaml
    /// </summary>
    public partial class Request : Window
    {
        public Request()
        {
            InitializeComponent();
        }

        private void BildReport_Click(object sender, RoutedEventArgs e)
        {
            ((RequestVM)DataContext).BildDepartmentReport();
        }
    }
}

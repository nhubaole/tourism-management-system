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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourismManagementSystem.ViewModel;
using TourismManagementSystem.UserControls;

namespace TourismManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for UCCustomer.xaml
    /// </summary>
    public partial class UCCustomer : UserControl
    {
        public UCCustomer()
        {
            InitializeComponent();
            DataContext = new CustomerVM();
        }
    }
}

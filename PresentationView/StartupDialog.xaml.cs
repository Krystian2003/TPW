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

namespace PresentationView
{
    public partial class StartupDialog : Window
    {
        public int BallCount { get; private set; }
        public StartupDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BallCountTextBox.Text, out int count) && count > 0)
            {
                BallCount = count;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

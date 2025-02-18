﻿using bankproject;
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
using System.Windows.Shapes;

namespace bankproject_GUI
{
    
    public partial class DonateWindow : Window
    {

        private Account user;

        public DonateWindow(Account user)
        {
            InitializeComponent();
            this.user = user;
            BalanceTextBox.Text = $"{user.Balance:C}"; 
        }

        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(DonateAmountTextBox.Text, out decimal depositAmount))
            {
                
                if(depositAmount<=0)
                {
                    MessageBox.Show($"The amount is invalid", "Donation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                    user.Deposit(depositAmount); 
                    BalanceTextBox.Text = $"{user.Balance:C}"; 
                    MessageBox.Show($"Successfully donated {depositAmount:C}.", "Transaction Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                    
                    this.DialogResult = true;
                    this.Close();  
            }
            else
            {
                MessageBox.Show("Invalid amount. Please enter a valid number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } // Donating Amount to account's balance

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        { 
            this.DialogResult = false;
            this.Close(); 
        } // Cancel whole function if Cancel clicked
    }
}

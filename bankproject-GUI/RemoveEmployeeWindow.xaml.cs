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
    public partial class RemoveEmployeeWindow : Window
    {

        private Bank bank;

        public RemoveEmployeeWindow(Bank bank)
        {
            InitializeComponent();
            this.bank = bank;
        }


        private void EmployeeIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmployeeIDWatermark.Visibility = string.IsNullOrEmpty(EmployeeIDTextBox.Text) ? Visibility.Visible : Visibility.Hidden; //hidding watermark  
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {    
            PasswordWatermark.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Hidden; //hidding watermark
        } 

        private void RemoveEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string employeeIDText = EmployeeIDTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(employeeIDText) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Employee ID and password!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            BankEmployee employeeToRemove = bank.employeesForXML.FirstOrDefault(e => e.EmployeeID == employeeIDText && e.EmployeePassword == password);

            if (employeeToRemove != null)
            {
                try
                {
                    bank.RemoveEmployee(employeeToRemove);
                    MessageBox.Show("Employee removed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing employee: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Employee not found with the provided ID and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } // Removing Employee from the list
    }
}

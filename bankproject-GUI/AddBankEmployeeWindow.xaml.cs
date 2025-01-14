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
    public partial class AddBankEmployeeWindow : Window
    {
        public BankEmployee? NewEmployee { get; private set; }
        public Bank bank { get; private set; }
        public AddBankEmployeeWindow(Bank bank)
        {

            InitializeComponent();
            this.bank = bank;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string name = NameTextBox.Text.Trim();
                string surname = SurnameTextBox.Text.Trim();
                string pesel = PeselTextBox.Text.Trim();
                if(bank.employeesForXML.Find(t=>t.Pesel==pesel)!= null || bank.accountsForXML.Find(t => t.Owner.Pesel==pesel)!=null)
                {
                    MessageBox.Show($"Account with this Pesel already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                EnumSex sex = (SexComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString() == "M" ? EnumSex.M : EnumSex.K;
                long employeeID = long.Parse(EmployeeIDTextBox.Text.Trim());
                string password = PasswordBox.Password.Trim();
                if (bank.employeesForXML.Find(t => t.EmployeePassword == password) != null || bank.accountsForXML.Find(t => t.Password == password) != null)
                {
                    MessageBox.Show($"Account with this password already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(pesel) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("All fields must be filled.");
                    return;
                }
                if(bank.bankEmployees.ContainsKey(employeeID))
                {
                    MessageBox.Show($"Employee with this ID already exists","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                NewEmployee = new BankEmployee(name, surname, pesel, sex, employeeID, password);

                
                DialogResult = true;
                Close();
            }
            catch (WrongPasswordException ex)
            {
                MessageBox.Show($"Password Error: {ex.Message}");
            }
            catch (FormatException)
            {
                MessageBox.Show("Employee ID must be a valid number.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }
    }
}

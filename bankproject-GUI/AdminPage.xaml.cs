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
using bankproject;



namespace bankproject_GUI
{
    
    public partial class AdminPage : Page
    {
        #region GlobalVariables
        BankEmployee employee;

        MainWindow mainWindow;

        private Bank bank;
        #endregion


        public AdminPage(MainWindow mainWindow, Bank bank)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            employee = mainWindow.LoggedInBankEmployee;
            this.bank = bank;
            AccountList.ItemsSource = new ObservableCollection<Account>(bank.accountsForXML);
            
        }


        private void LogoutButton_Click(Object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(new LoginPage(mainWindow,bank));
            bank.SaveXml("../../../../MyBank.xml");
            bank.accounts.Clear();
            bank.bankEmployees.Clear();
            bank.accountsForXML.Clear();
            bank.employeesForXML.Clear();

        } // Saving changes and going back to loginPage

        private void SortButton_Click(Object sender, RoutedEventArgs e) // Sorting List of accounts on their balance
        {
            bank.Sort();
            AccountList.ItemsSource = new ObservableCollection<Account>(bank.accountsForXML);

        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e) //Showing new window where we can add Account
        {
            AddAccountWindow addAccountWindow = new AddAccountWindow(bank);
            addAccountWindow.ShowDialog();

            if (addAccountWindow.AccountAdded)
            {
                AccountList.ItemsSource = new ObservableCollection<Account>(bank.accountsForXML);
            }
        }

        private void RemoveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveAccountWindow removeAccountWindow = new RemoveAccountWindow(bank);
            removeAccountWindow.ShowDialog();

            AccountList.ItemsSource = new ObservableCollection<Account>(bank.accountsForXML);
        } // Showing new window where we can Remove Account

        private void AddNewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddBankEmployeeWindow addBankEmployeeWindow = new AddBankEmployeeWindow(bank);
            if (addBankEmployeeWindow.ShowDialog() == true) 
            {
                if (addBankEmployeeWindow.NewEmployee != null)
                {
                    try
                    {
                        bank.AddEmployee(addBankEmployeeWindow.NewEmployee);
                        MessageBox.Show("Bank employee added successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding employee: {ex.Message}");
                    }
                }
            }
        }   // Showing new window where we can Add Employee

        private void RemoveEmployeeButton_Click(object sender, RoutedEventArgs e)
        { 
            RemoveEmployeeWindow removeEmployeeWindow = new RemoveEmployeeWindow(bank);
            removeEmployeeWindow.ShowDialog();
        } // Showing new window where we can Remove Employee
    }
}

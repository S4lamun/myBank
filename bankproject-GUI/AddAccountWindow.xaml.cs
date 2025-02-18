using bankproject;
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
    public partial class AddAccountWindow : Window
    {
        #region GlobalVariables
        private Bank bank;
        public bool AccountAdded { get; private set; } = false;
        #endregion


        public AddAccountWindow(Bank bank)
        {
            InitializeComponent();
            this.bank = bank;
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string pesel = PeselTextBox.Text;

                if (bank.accountsForXML.Find(t => t.Owner.Pesel == pesel) != null || bank.employeesForXML.Find(t => t.Pesel == pesel) != null)
                {
                    MessageBox.Show("Account with this PESEL already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedSexItem = SexComboBox.SelectedItem as ComboBoxItem;
                EnumSex sex = selectedSexItem?.Tag?.ToString() == "M" ? EnumSex.M : EnumSex.K;

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                    string.IsNullOrEmpty(pesel) || SexComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please fill all customer fields!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
              
                var newCustomer = new BankCustomer(firstName, lastName, pesel, sex);
                string login = LoginTextBox.Text;
                string password = PasswordBox.Password;

                if (bank.accountsForXML.Find(t => t.Password == password) != null || bank.employeesForXML.Find(t=>t.EmployeePassword==password)!=null)
                {
                    MessageBox.Show("Account with this password already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(BalanceTextBox.Text, out decimal balance))
                {
                    MessageBox.Show("Invalid balance value!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               
                if (balance<=0)
                {
                    MessageBox.Show("Invalid Amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter a password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newAccount = new Account(newCustomer, password, balance, login);  
                bank.AddAccount(newAccount);
                AccountAdded = true; 

                MessageBox.Show("Account added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } // Adding Account to List
    }
}
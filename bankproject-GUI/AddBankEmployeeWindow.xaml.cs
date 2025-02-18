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
    public partial class AddBankEmployeeWindow : Window
    {
        #region GlobalVariables
        public BankEmployee? NewEmployee { get; private set; }
        public Bank bank { get; private set; }
        #endregion

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
                string password = PasswordBox.Password.Trim();

                if (bank.employeesForXML.Find(t => t.EmployeePassword == password) != null || bank.accountsForXML.Find(t => t.Password == password) != null)
                {
                    MessageBox.Show($"Account with this password already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(pesel) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("All fields must be filled.");
                    return;
                }

                NewEmployee = new BankEmployee(name, surname, pesel, sex, password);
                MessageBox.Show($"Your ID (your login) is: {NewEmployee.EmployeeID}", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (WrongPasswordException ex)
            {
                MessageBox.Show($"Invalid Password, Password must have 6 characters including 1 Capital letter and 1 digit");
            }
            catch (FormatException)
            {
                MessageBox.Show("Employee ID must be a valid number.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        } // Adding Employ to list
    }
}

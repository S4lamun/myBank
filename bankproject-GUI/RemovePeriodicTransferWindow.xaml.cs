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
    
    public partial class RemovePeriodicTransferWindow : Window
    {
        Account user;
        Bank bank;

        public RemovePeriodicTransferWindow(MainWindow mainWindow, Account user, Bank bank)
        {
            InitializeComponent();
            this.user = user;
            this.bank = bank;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string accountNumber = AccountNumberTextBox.Text.Trim();
            string intervalString = IntervalTextBox.Text.Trim();
            string amountString = AmountTextBox.Text.Trim();


            if (!int.TryParse(intervalString, out int interval))
            {
                MessageBox.Show("Invalid amount of days", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(amountString, out decimal amount))
            {
                MessageBox.Show("Invalid amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(accountNumber) || accountNumber == user.AccountNumber || string.IsNullOrEmpty(intervalString) || string.IsNullOrEmpty(amountString))
            {
                MessageBox.Show("Fill all fields! (you can't send to yourself!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (bank.FindAccount(accountNumber) == null)
            {
                MessageBox.Show("There is no account with this number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var destinationAcc = bank.FindAccount(accountNumber);
            user.RemovePeriodicTransfer(destinationAcc, amount, interval);
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}

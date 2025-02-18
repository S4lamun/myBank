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
    
    public partial class WithdrawWindow : Window
    {

        private Account user;

        public WithdrawWindow(Account user)
        {
            InitializeComponent();
            this.user = user;
            BalanceTextBox.Text = $"{user.Balance:C}"; 
        }

        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(WithdrawAmountTextBox.Text, out decimal withdrawAmount))
            {
                try
                {
                    if (withdrawAmount <= 0)
                    {
                        MessageBox.Show($"The amount is invalid", "Withdraw Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    user.Withdraw(withdrawAmount); 
                    BalanceTextBox.Text = $"{user.Balance:C}"; 
                    MessageBox.Show($"Successfully withdrew {withdrawAmount:C}.", "Transaction Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close(); 
                }
                catch (WrongAmountException ex)
                {
                    MessageBox.Show(ex.Message, "Transaction Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid amount. Please enter a valid number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } // Withdrawing Amount from account

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        } // Cancel function if Cancel clicked
    }
}

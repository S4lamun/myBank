using bankproject;
using System;
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
using System.Windows.Shapes;

namespace bankproject_GUI
{
    /// <summary>
    /// Logika interakcji dla klasy PeriodicTransferWindows.xaml
    /// </summary>
    public partial class PeriodicTransferWindows : Window
    {
        Bank bank;
        Account user;
        MainWindow MainWindow;
        public PeriodicTransferWindows(MainWindow mainWindow, Bank bank)
        {
            InitializeComponent();

            user = mainWindow.LoggedInUser;
            this.bank = bank;
            this.MainWindow = mainWindow;
            NameBlock.Text = "myBank";
            Name1Block.Text = "Manage your Periodic Transfers";
            TransferList.ItemsSource = new ObservableCollection<PeriodicTransfer>(user.periodicTransfers);
            
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPeriodicTransferWindow addPeriodicTransfer = new AddPeriodicTransferWindow(MainWindow, user, bank);
            bool? result = addPeriodicTransfer.ShowDialog();
            if (result == true)
            {
                TransferList.ItemsSource = new ObservableCollection<PeriodicTransfer>(user.periodicTransfers);
            }
            

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            RemovePeriodicTransferWindow removePeriodicTransfer = new RemovePeriodicTransferWindow(MainWindow, user, bank);
            bool? result = removePeriodicTransfer.ShowDialog();
            if (result == true)
            {
                TransferList.ItemsSource = new ObservableCollection<PeriodicTransfer>(user.periodicTransfers);
            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            user.ExecuteAllPeriodicTransfers();
            TransferList.ItemsSource = new ObservableCollection<PeriodicTransfer>(user.periodicTransfers);
            MessageBox.Show("All needed Transfer were made", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }
    }
}

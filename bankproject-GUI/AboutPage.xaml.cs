using System.Windows;
using System.Windows.Controls;

namespace bankproject_GUI
{
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            Text1Block.Text = "Project is a simple simulation of a bank. You can log in as Admin or a Typical User.\nAs a Typical User, you can:\nCheck your Name and Balance\nWithdraw, " +
                "Transfer, and Deposit money\nMake Periodic Transfers\nCheck Transaction History\n\nAs an Admin, you can manage Accounts:\nAdd new Account\nRemove Account\nAdd Employee\nRemove Employee";

            Text2Block.Text = "Account Numbers are generated randomly with 26 digits combined as a string.\nBank Employee IDs are generated automatically (it’s the admin's login).\nUser Logins must be unique.";

            Text3Block.Text = "Login Data: [Login Password] (1 for admin and 1 for user)\n" +
                              "              [james.miller54 3u1wyE] - user\n" +
                              "              [mary.williams97 9S2L1s] - admin";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic to navigate back to the previous page
            NavigationService.GoBack();
        }
    }
}

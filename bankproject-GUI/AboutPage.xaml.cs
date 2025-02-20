using System.Windows;
using System.Windows.Controls;

namespace bankproject_GUI
{
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            Text1Block.Text = "💰 **About the Project**\nThis project is a simple bank simulation. You can log in as an **Administrator** or a **Regular User**.\n\n"
                            + "🧑‍💼 **As a User, you can:**\n"
                            + "- Check your name and balance\n"
                            + "- Withdraw, transfer, and deposit money\n"
                            + "- Set up periodic transfers\n"
                            + "- View transaction history\n\n"
                            + "👨‍💻 **As an Administrator, you can manage accounts:**\n"
                            + "- Add new accounts\n"
                            + "- Remove accounts\n"
                            + "- Add employees\n"
                            + "- Remove employees";

            Text2Block.Text = "🔢 **Account Numbers & Identifiers**\n"
                            + "- Account numbers are randomly generated (26 characters).\n"
                            + "- Employee IDs are generated automatically.\n"
                            + "- User logins must be unique.";

            Text3Block.Text = "🔐 **Login Credentials**\n"
                            + "- **User:** `james.miller54` | **Password:** `3u1wyE`\n"
                            + "- **Admin:** `mary.williams97` | **Password:** `9S2L1s`";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
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
using System.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using WpfAnimatedGif;
namespace bankproject_GUI
{
    
    public partial class LoginPage : Page
    {
        #region GlobalVariables
        private MediaPlayer m_MediaPlayer;

        private MediaPlayer e_MediaPlayer;

        private MainWindow mainWindow;

        private Bank b1;
        #endregion


        public LoginPage(MainWindow mainWindow, Bank bank)
        {
            m_MediaPlayer = new MediaPlayer();
            e_MediaPlayer = new MediaPlayer();
            string soundPath = System.IO.Path.GetFullPath(@"..\..\..\..\Images\button.mp3");
            string errorPath = System.IO.Path.GetFullPath(@"..\..\..\..\Images\error.mp3");
            if (File.Exists(soundPath))
            {
                m_MediaPlayer.Open(new Uri(soundPath));
            }
            else
            {
                MessageBox.Show("Nie znaleziono pliku dźwiękowego!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (File.Exists(errorPath))
            {
                e_MediaPlayer.Open(new Uri(errorPath));
            }
            else
            {
                MessageBox.Show("Nie znaleziono pliku dźwiękowego!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            InitializeComponent();
            this.b1 = bank;
            this.mainWindow = mainWindow;
            var gifSource = new Uri("pack://application:,,,/loading.gif"); // Adjust path as needed
            ImageBehavior.SetAnimatedSource(LoadingGif, new System.Windows.Media.Imaging.BitmapImage(gifSource));

            ImageBehavior.SetRepeatBehavior(LoadingGif, RepeatBehavior.Forever);

        }

        private void ShowLoadingGif(bool isVisible)
        {
            LoadingGif.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        } // showing and hiding GIF
       
        private void PlayClickSound()
        {
            if (m_MediaPlayer.Source != null)
            {
                m_MediaPlayer.Position = TimeSpan.Zero;
                m_MediaPlayer.Play();
            }
        } // ClickSound

        private void PlayClickSound2()
        {
            if (e_MediaPlayer.Source != null)
            {
                e_MediaPlayer.Position = TimeSpan.Zero;
                e_MediaPlayer.Play();
            }
        } // ErrorSound 

        private async void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            b1.ReadXml("../../../../MyBank.xml"); // Reading file with save

            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (b1.FindAccount(login, password) is not null) // searching for account (user)
            {
                ShowLoadingGif(true);

                await Task.Delay(2000);
                ShowLoadingGif(false);
                ImageBehavior.SetAutoStart(LoadingGif, false);
                PlayClickSound();
                mainWindow.LoggedInUser = b1.FindAccount(login, password);
                mainWindow.MainFrame.Navigate(new UserPage(mainWindow, b1));
            }
           else if (b1.FindEmployee(login, password) is not null) // searching for account (admin)
            {
                ShowLoadingGif(true);
                await Task.Delay(2000);
                ShowLoadingGif(false);
                ImageBehavior.SetAutoStart(LoadingGif, false);
                PlayClickSound();

                mainWindow.MainFrame.Navigate(new AdminPage(mainWindow, b1));
                mainWindow.LoggedInBankEmployee = b1.FindEmployee(login, password);
            }
            else // if there is no admin or user 
            {
                PlayClickSound2();
                NoAccountWindow noAccountWindow = new NoAccountWindow();
                noAccountWindow.ShowDialog();
                b1.accounts.Clear();
                b1.bankEmployees.Clear();
                b1.accountsForXML.Clear();
                b1.employeesForXML.Clear();

            }


        }
        
    }
}

using bankproject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Media;
namespace bankproject_GUI
{
   
    public partial class MainPage : Page
    {
        #region GlobalVariables
        Bank b1 = new("myBank"); // Making a instance of Bank

        private MediaPlayer m_MediaPlayer;

        private MainWindow mainWindow;
        #endregion

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            
            this.mainWindow = mainWindow;
            
            m_MediaPlayer = new MediaPlayer();
            string soundPath = System.IO.Path.GetFullPath(@"..\..\..\..\Images\button.mp3");

            if (File.Exists(soundPath))
            {
                m_MediaPlayer.Open(new Uri(soundPath));
            }
            else
            {
                MessageBox.Show("Nie znaleziono pliku dźwiękowego!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void PlayClickSound()
        {
            if (m_MediaPlayer.Source != null)
            {
                m_MediaPlayer.Position = TimeSpan.Zero; 
                m_MediaPlayer.Play();
            }
        } // Making a Sound to play
        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            PlayClickSound();
            if (mainWindow == null)
            {
                MessageBox.Show("MainWindow reference is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            mainWindow.MainFrame.Navigate(new LoginPage(mainWindow, b1));
        } // Opening Page where we can log in
        
        
        private void AboutButton_Click(Object sender, RoutedEventArgs e)
        {
            PlayClickSound();

            mainWindow.MainFrame.Navigate(new AboutPage());

        } // Showing README
        
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            PlayClickSound();
            Application.Current.Shutdown();
        } // Shutting down whole App

    }
}

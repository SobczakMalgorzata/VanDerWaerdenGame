using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace VanDerWaerdenGame.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public MainWindowViewModel ViewModel => this.DataContext as MainWindowViewModel;

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartNewGame();
        }
            
        private void NextTurnButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(ViewModel.Turn);
        }
        
        private void PlayTillEndButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(ViewModel.PlayTillEnd);
        }

        private void TrainPlayersButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(ViewModel.TrainPositionPlayer);
            //Task.Factory.StartNew(ViewModel.TrainPlayers);
        }

        private void ResetAndPlayTillEnd_Click(object sender, RoutedEventArgs e)
        {
            StartGameButton_Click(sender, e);
            PlayTillEndButton_Click(sender, e);
        }

        private void TestPlayersButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TestPlayers();
        }

        private void TestToFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Comma separated values files (*.csv)|*.csv";

            if (dialog.ShowDialog() == true)
            {//Task.Factory.StartNew(() =>ViewModel.TestPlayers(dialog.FileName));
                if (File.Exists(dialog.FileName))
                    File.Delete(dialog.FileName);
                ViewModel.TestPlayers(dialog.FileName);
            }
        }
    }
}

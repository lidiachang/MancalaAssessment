using MancalaAssessment.ViewModels;
using System;
using System.Windows;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardViewModel boardViewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            boardViewModel = new BoardViewModel();
            GameBoard.DataContext = boardViewModel;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            boardViewModel = new BoardViewModel();
            GameBoard.DataContext = boardViewModel;
            GameBoard.PitsPlayer1.IsEnabled = true;
            GameBoard.PitsPlayer2.IsEnabled = true;

            //throw new NotImplementedException();
        }
        public void Display(string player)
        {
            (this.DataContext as MainWindowViewModel).BannerText = player;
        }
    }
}

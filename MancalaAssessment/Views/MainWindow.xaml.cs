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
        }
        private void AI_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel("Play with AI, You(Player 1) go first.");
            boardViewModel = new BoardViewModel(true);
            boardViewModel.Player2Enable = false;
            GameBoard.DataContext = boardViewModel;
        }
    }
}

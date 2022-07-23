using System;
using System.Windows;
using System.Windows.Controls;
using MancalaAssessment.ViewModels;
using System.Collections.Generic;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();
        }

        private void MancalaPitButton_Click(object sender, RoutedEventArgs e)
        {
            BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);
            //get no. of stones from the view
            ////change the no. "only" on view
            int selected = ((Pit)sender).Distance;
            ((Pit)sender).Stones = 100;

            //get no of stones from ViewModel
            int p0 = ((sender as Pit).DataContext as BoardViewModel).StonesPlayer1[0];
            int p1 = ((sender as Pit).DataContext as BoardViewModel).StonesPlayer1[1];
            int p2 = ((sender as Pit).DataContext as BoardViewModel).StonesPlayer1[2];
            int p3 = ((sender as Pit).DataContext as BoardViewModel).StonesPlayer1[3];

            ////change the no. in viewmodel 
            ((sender as Pit).DataContext as BoardViewModel).StorePlayer1 = 5;
            ((sender as Pit).DataContext as BoardViewModel).StonesPlayer1[1] = 1000;
            int p5 = ((sender as Pit).DataContext as BoardViewModel).StonesPlayer2[1];
            //throw new NotImplementedException();
        }
        public bool Play(int i, List<int> Player, List<int> Opponent, ref int PlayerPool,ref int OpponentPool)
        {
            bool myside = true;
            bool rtn;
            int stones = Player[i];
            if (stones - (i + 1) % 14 == 0)
                rtn = true;
            else
                rtn = false;
            int j = i;
            Player[i] = 0;
            //distribute stones to players pit
            while (stones > 0)
            {
                if (j - 1 >= 0)
                {
                    j--;
                    Player[j]++;
                    stones--;
                }
                else if (j == 0)
                {
                    // add to the pool,
                    PlayerPool++;
                    stones--;
                    j = 6;
                    // if there's still stones after -- exchange player,j=6
                    // if not, conti.( end loop)
                    if (stones > 0)
                    {
                        myside = myside ? false : true;
                        var temp = Player;
                        var tempPool = PlayerPool;
                        Player = Opponent;
                        PlayerPool = OpponentPool;
                        Opponent = temp;
                        OpponentPool = tempPool;
                    }
                }
            }
            //check result a. if final stone stop at somewhere empty, myside & with opposite pit stones > 0
            if (myside == true && j != 6 && Player[j] == 1 && Opponent[5 - j] > 0)
            {
                PlayerPool = PlayerPool + Player[j] + Opponent[5 - j];
                Player[j] = 0;
                Opponent[5 - j] = 0;
            }
            //board.StorePlayer2 = myside ? PlayerPool : OpponentPool;
            //board.StorePlayer1 = myside == false ? PlayerPool : OpponentPool;

            return rtn;
        }
        public bool Player2(BoardViewModel board, int i)
        {
            bool myside = true;
            int stones = board.StonesPlayer2[i];
            bool rtn;
            if (stones - (i + 1) % 14 == 0)
                rtn = true;
            else
                rtn = false;
            var Player = board.StonesPlayer2;
            var Opponent = board.StonesPlayer1;

            var PlayerPool = board.StorePlayer2;
            var OpponentPool = board.StorePlayer1;
            //current player :2 
            int j = i;
            board.StonesPlayer2[i] = 0;
            //distribute stones to players pit
            while (stones > 0)
            {
                if (j - 1 >= 0)
                {
                    //distribute stones
                    j--;
                    Player[j]++;
                    stones--;
                }
                else if (j == 0)
                {
                    // add to the pool,
                    PlayerPool++;
                    stones--;
                    j = 6;
                    // if there's still stones after -- exchange player,j=6
                    // if not, conti.( end loop)
                    if (stones > 0)
                    {
                        myside = myside ? false : true;
                        var temp = Player;
                        var tempPool = PlayerPool;
                        Player = Opponent;
                        PlayerPool = OpponentPool;
                        Opponent = temp;
                        OpponentPool = tempPool;
                    }
                }
            }
            //check result a. if final stone stop at somewhere empty, myside & with opposite pit stones > 0
            if (myside == true && j != 6 && Player[j] == 1 && Opponent[5 - j] > 0)
            {
                PlayerPool = PlayerPool + Player[j] + Opponent[5 - j];
                Player[j] = 0;
                Opponent[5-j] = 0;
            }

            board.StorePlayer2 = myside ? PlayerPool : OpponentPool;
            board.StorePlayer1 = myside == false ? PlayerPool : OpponentPool;

            return rtn;
        }
        public bool Player1(BoardViewModel board, int i)
        {
            bool myside = true;
            int stones = board.StonesPlayer1[i];
            bool rtn;
            if (stones - (i + 1) % 14 == 0)
                rtn = true;
            else
                rtn = false;
            var Player = board.StonesPlayer1;
            var Opponent = board.StonesPlayer2;

            var PlayerPool = board.StorePlayer1;
            var OpponentPool = board.StorePlayer2;
            //current player :1 
            int j = i;
            board.StonesPlayer1[i] = 0;
            //distribute stones to players pit
            while (stones > 0)
            {
                if (j - 1 >= 0)
                {
                    //distribute stones
                    j--;
                    Player[j]++;
                    stones--;
                }
                else if (j == 0)
                {
                    // add to the pool,
                    PlayerPool++;
                    stones--;
                    j = 6;
                    // if there's still stones after -- exchange player,j=6
                    // if not, conti.( end loop)
                    if (stones > 0)
                    {
                        myside = myside ? false : true;
                        var temp = Player;
                        var tempPool = PlayerPool;
                        Player = Opponent;
                        PlayerPool = OpponentPool;
                        Opponent = temp;
                        OpponentPool = tempPool;
                    }
                }
            }
            //check result a. if final stone stop at somewhere empty, myside & with opposite pit stones > 0
            if (myside == true && j != 6 && Player[j] == 1 && Opponent[5 - j] > 0)
            {
                PlayerPool = PlayerPool + Player[j] + Opponent[5 - j];
                Player[j] = 0;
                Opponent[5 - j] = 0;
            }

            board.StorePlayer1 = myside ? PlayerPool : OpponentPool;
            board.StorePlayer2 = myside == false ? PlayerPool : OpponentPool;

            return rtn;
        }
        private bool CheckOver(BoardViewModel board)
        {
            // check result 
            bool isP1Empty = true;
            foreach (var s in board.StonesPlayer1)
            {
                if (s > 0)
                    isP1Empty = false;
            }
            bool isP2Empty = true;
            foreach (var s in board.StonesPlayer2)
            {
                if (s > 0)
                    isP2Empty = false;
            }
            if (isP1Empty)
            {
                for (int i = 0; i < 6; i++)
                {
                    board.StorePlayer2 = board.StorePlayer2 + board.StonesPlayer2[i];
                    board.StonesPlayer2[i] = 0;
                }
                return true;
            }
            if (isP2Empty)
            {
                for (int i = 0; i < 6; i++)
                {
                    board.StorePlayer1 = board.StorePlayer1 + board.StonesPlayer1[i];
                    board.StonesPlayer1[i] = 0;
                }

                return true;
            }

            return false;
        }
        private void MancalaPitButton_Click20(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 0;
            //current player :2 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);
                CheckResult(data, Player2(data, i),2);
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                //parentWindow.CheckResult(rtn ? "Player 2" : "Player 1");
                
            }

        }
        private void MancalaPitButton_Click21(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 1;
            //current player :2 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);


                CheckResult(data, Player2(data, i),2);
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                //parentWindow.CheckResult(rtn ? "Player 2" : "Player 1");
            }
        }
        private void MancalaPitButton_Click22(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 2;
            //current player :2 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

               CheckResult(data, Player2(data, i),2);
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                //parentWindow.CheckResult(rtn ? "Player 2" : "Player 1");
            }
        }
        private void MancalaPitButton_Click23(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 3;
            //current player :2 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);
                CheckResult(data, Player2(data, i),2);
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                //parentWindow.CheckResult(rtn ? "Player 2" : "Player 1");
                
            }
        }
        private void MancalaPitButton_Click24(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 4;
            //current player :2 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player2(data, i),2);
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                //parentWindow.CheckResult(rtn ? "Player 2" : "Player 1");
            }
        }
        private void MancalaPitButton_Click25(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 5;
            //current player :2 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player2(data, i),2);
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                //parentWindow.CheckResult(rtn ? "Player 2" : "Player 1");
            }
        }
        private void MancalaPitButton_Click10(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 0;
            //current player :1 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player1(data, i),1);
            }
        }
        private void MancalaPitButton_Click11(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 1;
            //current player :1 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player1(data, i),1);
            }
        }
        private void MancalaPitButton_Click12(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 2;
            //current player :1 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player1(data, i),1);
            }
        }
        private void MancalaPitButton_Click13(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 3;
            //current player :1 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player1(data, i),1);
            }
        }
        private void MancalaPitButton_Click14(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 4;
            //current player :1 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player1(data, i),1);
            }
        }
        private void MancalaPitButton_Click15(object sender, RoutedEventArgs e)
        {
            // starting position
            int i = 5;
            //current player :1 
            if (sender is Pit && (sender as Pit).Stones > 0)
            {
                
                BoardViewModel data = ((sender as Pit).DataContext as BoardViewModel);

                CheckResult(data, Player1(data, i), 1);
            }
        }

        private void CheckResult(BoardViewModel board, bool touchStore,int player)
        {
            string result = "";
            // check if game over 
            //(any player's pits are empty  => opponent get all))
            //if game over
            // ==> determine who wins
            //else 
            // ==> show who's turn


            // ==> change side or not
            if (CheckOver(board))
            {
                if (board.StorePlayer1 > board.StorePlayer2)
                {
                    result = "Player 1 Wins ! Click New Game to restart.";
                    PitsPlayer2.IsEnabled = false;
                    PitsPlayer1.IsEnabled = false;
                }
                else if (board.StorePlayer1 < board.StorePlayer2)
                {
                    result = "Player 2 Wins ! Click New Game to restart.";
                    PitsPlayer2.IsEnabled = false;
                    PitsPlayer1.IsEnabled = false;
                }
                else
                {
                    result = "TIE ! Click New Game to restart.";
                    PitsPlayer2.IsEnabled = false;
                    PitsPlayer1.IsEnabled = false;
                }
            }
            else
            {
                if (player == 2)
                {
                    if (touchStore)
                    {
                        PitsPlayer2.IsEnabled = true;
                        PitsPlayer1.IsEnabled = false;
                        result = "Player 2";
                    }
                    else
                    {
                        PitsPlayer2.IsEnabled = false;
                        PitsPlayer1.IsEnabled = true;
                        result = "Player 1";
                    }
                }
                else
                {
                    if (touchStore)
                    {
                        PitsPlayer2.IsEnabled = false;
                        PitsPlayer1.IsEnabled = true;
                        result = "Player 1";
                    }
                    else
                    {
                        PitsPlayer2.IsEnabled = true;
                        PitsPlayer1.IsEnabled = false;
                        result = "Player 2";
                    }
                }
            }
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.Display(result);
        }
    }
}

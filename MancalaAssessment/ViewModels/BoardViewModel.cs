using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Linq;
namespace MancalaAssessment.ViewModels
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public BoardViewModel(bool AI = false)
        {
            playwithAI = AI;
            MovePlayer1 = new RelayCommand(p => Play(int.Parse(p.ToString()), 1));
            MovePlayer2 = new RelayCommand(p => Play(int.Parse(p.ToString()), 2));
        }
        private ObservableCollection<int> stonesPlayer1 = new ObservableCollection<int>() { 4, 4, 4, 4, 4, 4 };
        public ObservableCollection<int> StonesPlayer1
        {
            get => stonesPlayer1;
            set
            {
                if (value != stonesPlayer1)
                {
                    stonesPlayer1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StonesPlayer1)));
                }
            }
        }

        private ObservableCollection<int> stonesPlayer2 = new ObservableCollection<int>() { 4, 4, 4, 4, 4, 4 };
        public ObservableCollection<int> StonesPlayer2
        {
            get => stonesPlayer2;
            set
            {
                if (value != stonesPlayer2)
                {
                    stonesPlayer2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StonesPlayer2)));
                }
            }
        }

        private int storePlayer1 = 0;
        public int StorePlayer1
        {
            get => storePlayer1;
            set
            {
                if (value != storePlayer1)
                {
                    storePlayer1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StorePlayer1)));
                }
            }
        }

        private int storePlayer2 = 0;
        public int StorePlayer2
        {
            get => storePlayer2;
            set
            {
                if (value != storePlayer2)
                {
                    storePlayer2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StorePlayer2)));
                }
            }
        }
        //Enable lable to control button display.
        private bool player1Enable = true;
        private bool player2Enable = true;
        public bool Player1Enable
        {
            get => player1Enable;
            set
            {
                if (value != player1Enable)
                {
                    player1Enable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(player1Enable)));
                }
            }
        }
        public bool Player2Enable
        {
            get => player2Enable;
            set
            {
                if (value != player2Enable)
                {
                    player2Enable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(player2Enable)));
                }
            }
        }
        public ICommand MovePlayer1 { set; get; }
        public ICommand MovePlayer2 { set; get; }

        private bool playwithAI = false;

        private string result = "";


        public void Play(int i, int player)
        {
            //set initial player & opponent
            int stones = player == 1 ? stonesPlayer1[i] : stonesPlayer2[i];
            if (stones == 0)
                return;

            var Player = player == 1 ? stonesPlayer1 : stonesPlayer2;
            var Opponent = player == 1 ? stonesPlayer2 : stonesPlayer1;

            var PlayerPool = player == 1 ? storePlayer1 : storePlayer2;
            var OpponentPool = player == 1 ? storePlayer2 : storePlayer1;

            bool myside = true;
            bool touchStore;

            if (stones - (i + 1) % 14 == 0)
                touchStore = true;
            else
                touchStore = false;

            int j = i;

            if (player == 1)
                stonesPlayer1[i] = 0;
            else
                stonesPlayer2[i] = 0;

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
                    j = 6;
                    if (myside)
                    {
                        // add to the pool,
                        PlayerPool++;
                        stones--;
                    }
                    // if there's still stones after -- exchange player
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
            //Trigger Porperty changed
            if (player == 1)
            {
                StorePlayer1 = myside ? PlayerPool : OpponentPool;
                StorePlayer2 = myside == false ? PlayerPool : OpponentPool;
            }
            else
            {
                StorePlayer2 = myside ? PlayerPool : OpponentPool;
                StorePlayer1 = myside == false ? PlayerPool : OpponentPool;
            }
            result = CheckResult(touchStore, player);

            // If it's with AI. Automatically move player2
            if (result == "Player 2" && playwithAI)
            {
                Task<string> AIPlayR = AIPlay();
            }
            //send messenger to MainWindowViewModel where there register a receive handler.
            Messenger.Default.Send(result);
        }
        private bool CheckOver()
        {
            // check result 
            bool isP1Empty = true;
            foreach (var s in StonesPlayer1)
            {
                if (s > 0)
                    isP1Empty = false;
            }
            bool isP2Empty = true;
            foreach (var s in StonesPlayer2)
            {
                if (s > 0)
                    isP2Empty = false;
            }
            if (isP1Empty)
            {
                for (int i = 0; i < 6; i++)
                {
                    StorePlayer2 = StorePlayer2 + StonesPlayer2[i];
                    StonesPlayer2[i] = 0;
                }
                return true;
            }
            if (isP2Empty)
            {
                for (int i = 0; i < 6; i++)
                {
                    StorePlayer1 = StorePlayer1 + StonesPlayer1[i];
                    StonesPlayer1[i] = 0;
                }

                return true;
            }

            return false;
        }
        private string CheckResult(bool touchStore, int player)
        {
            string result = "";
            // check if game over 
            //(any player's pits are empty  => opponent get all))
            //if game over
            // ==> determine who wins
            //else 
            // ==> show who's turn


            if (CheckOver())
            {
                if (StorePlayer1 > StorePlayer2)
                {
                    result = "Player 1 Wins ! Click New Game to restart.";
                    Player2Enable = false;
                    Player1Enable = false;
                }
                else if (StorePlayer1 < StorePlayer2)
                {
                    result = "Player 2 Wins ! Click New Game to restart.";
                    Player2Enable = false;
                    Player1Enable = false;
                }
                else
                {
                    result = "TIE ! Click New Game to restart.";
                    Player2Enable = false;
                    Player1Enable = false;
                }
            }
            else
            {
                if (player == 2)
                {
                    if (touchStore)
                    {
                        Player2Enable = true;
                        Player1Enable = false;
                        result = "Player 2";
                    }
                    else
                    {
                        Player2Enable = false;
                        Player1Enable = true;
                        result = "Player 1";
                    }
                }
                else
                {
                    if (touchStore)
                    {
                        Player2Enable = false;
                        Player1Enable = true;
                        result = "Player 1";
                    }
                    else
                    {
                        Player2Enable = true;
                        Player1Enable = false;
                        result = "Player 2";
                    }
                }
            }
            return result;
        }
        private async Task<string> AIPlay()
        {
            await Task.Delay(1500);
            int i = AI_SelectPit();
            Play(i, 2);
            return "Player2 Complete";
        }
        private int AI_SelectPit()
        {
            int selected = -1;

            while (selected < 0)
            {
                for (int i = 0; i < 6 ; i++)
                {
                    //Pick one that could gain a free move
                    if (stonesPlayer2[i] == i + 1)
                    {
                        selected = i;
                        return selected;
                    }
                }
                //move one that can eat opponent's stones 
                for (int i = 5; i > 0; i--)
                {
                    int j = i - (stonesPlayer2[i]);
                    if (j >= 0 && stonesPlayer2[i] > 0 && stonesPlayer2[j] == 0 && stonesPlayer2[i] == j && stonesPlayer1[5 - i] > 2)
                    {
                        selected = i;
                        return selected;
                    }
                }
                
                    // pick one closer to pool
                    for (int i = 0; i < 6; i++)
                    {
                        if (stonesPlayer2[i] > 3 && stonesPlayer2[i]>i+1)
                        {
                            selected = i;
                            return selected;
                        }
                    }
                
                //move one that has a large number & could be eaten by opponent 
                for (int i = 0; i < 6; i++)
                {
                    if (stonesPlayer2[i] > 3 && stonesPlayer1[5 - i] == 0)
                    {
                        for (int j = 5; j > (5 - i); j--)
                        {
                            if (stonesPlayer1[j] == j - (5 - i))
                            {
                                selected = i;
                                return selected;
                            }
                        }
                    }
                }

                // pick one closer to pool
                for (int i = 0; i < 6; i++)
                {
                    if (stonesPlayer2[i] > 0)
                    {
                        selected = i;
                        return selected;
                    }
                }
            }

            return stonesPlayer2.IndexOf(stonesPlayer2.Where(x => x > 0).First());

        }
    }

}

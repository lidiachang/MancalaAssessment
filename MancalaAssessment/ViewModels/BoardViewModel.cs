using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MancalaAssessment.ViewModels
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public BoardViewModel()
        {
            //ChangeMessage = new ChangeMessageCommand(this);
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

        public int selectedIndex = 0;
        public ICommand MovePlayer1 { set; get; }
        public ICommand MovePlayer2 { set; get; }

        public bool Play(int i, int player)
        {
            int stones = player == 1 ? stonesPlayer1[i] : stonesPlayer2[i];
            var Player = player == 1 ? stonesPlayer1 : stonesPlayer2;
            var Opponent = player == 1 ? stonesPlayer2 : stonesPlayer1;

            var PlayerPool = player == 1 ? storePlayer1 : storePlayer2;
            var OpponentPool = player == 1 ? storePlayer2 : storePlayer1;

            bool myside = true;
            bool rtn;
            if (stones - (i + 1) % 14 == 0)
                rtn = true;
            else
                rtn = false;

            int j = i;

            if(player==1)
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
            bool gameOver = CheckOver();
            return rtn;
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
            foreach (var s in StonesPlayer1)
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
    }
}

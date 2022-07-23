using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MancalaAssessment.ViewModels
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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

    }
}

using System.ComponentModel;

namespace MancalaAssessment.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindowViewModel()
        {
            Messenger.Default.Register<string>(this, ChangeBanner);
        }
        public MainWindowViewModel(string customizedB)
        {
            Messenger.Default.Register<string>(this, ChangeBanner);
            ChangeBanner(customizedB);
        }
        private string bannerText = "Click \"New Game\" to get started!";
        public string BannerText
        {
            get => bannerText;
            set
            {
                if(bannerText != value)
                {
                    bannerText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BannerText)));
                }
            }
        }
        
        public void ChangeBanner(string text)
        {
            BannerText = text;
        }

    }
}

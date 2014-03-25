using GalaSoft.MvvmLight;
using StirTrekConf.WinPhone.Model;

namespace StirTrekConf.WinPhone.ViewModel
{
    using System.Collections.ObjectModel;
    using PortableCore.WebServiceLayer;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly WebServiceRepo _webServiceRepo;

        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }

        public MainViewModel(IDataService dataService, WebServiceRepo webServiceRepo)
        {
            _dataService = dataService;
            _webServiceRepo = webServiceRepo;

            _webServiceRepo.LoadStirTrekFeed();

            Speakers = new ObservableCollection<SpeakerViewModel>();


            _dataService.GetSpeakers(
                (speakers, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    foreach (var speak in speakers)
                    {
                        Speakers.Add(new SpeakerViewModel(speak));
                    }
                });
        }

        public ObservableCollection<SpeakerViewModel> Speakers { get; private set; }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}
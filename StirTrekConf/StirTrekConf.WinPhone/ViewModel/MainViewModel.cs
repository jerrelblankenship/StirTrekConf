using GalaSoft.MvvmLight;
using StirTrekConf.WinPhone.Model;

namespace StirTrekConf.WinPhone.ViewModel
{
    using System.Collections.ObjectModel;
    using PortableCore.WebServiceLayer;

    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly WebServiceRepo _webServiceRepo;

        public MainViewModel(IDataService dataService, WebServiceRepo webServiceRepo)
        {
            _dataService = dataService;
            _webServiceRepo = webServiceRepo;
            Speakers = new ObservableCollection<SpeakerViewModel>();

            _webServiceRepo.LoadStirTrekFeed(LoadSpeakerViewModel);
        }

        public void LoadSpeakerViewModel()
        {
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
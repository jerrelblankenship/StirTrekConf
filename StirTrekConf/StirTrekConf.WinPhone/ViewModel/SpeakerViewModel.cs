namespace StirTrekConf.WinPhone.ViewModel
{
    using GalaSoft.MvvmLight;
    using PortableCore.DomainLayer;

    public class SpeakerViewModel : ViewModelBase
    {
        public Speaker Speaker { get; private set; }
        public SpeakerViewModel(Speaker speaker)
        {
            Speaker = speaker;
        }

        public string DisplayName
        {
            get { return Speaker.Name; }
        }
    }
}
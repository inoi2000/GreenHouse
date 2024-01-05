using GreenHouse.HttpModels.Responses;

namespace GreenHouse.WebAdminClient
{
    public class AppState
    {
        public bool IsTokenChecked { get; set; }

        private bool _loggedIn;
        public event Action OnChange;

        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        public AdminResponse? Admin { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();

        public string Host { get; set; }

        public AppState(string host)
        {
            Host = host;
        }
    }
}

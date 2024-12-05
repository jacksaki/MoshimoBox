using Livet;
using Livet.Commands;
using MoshimoBox.Models;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace MoshimoBox.ViewModels.Pages
{
    public class SettingsViewModel : NotificationObject, INavigationAware
    {
        private bool _isInitialized = false;


        private string _AppVersion;

        public string AppVersion
        {
            get
            {
                return _AppVersion;
            }
            set
            {
                if (_AppVersion == value)
                {
                    return;
                }
                _AppVersion = value;
                RaisePropertyChanged();
            }
        }

        private ApplicationTheme _currentTheme;

        public ApplicationTheme CurrentTheme
        {
            get
            {
                return _currentTheme;
            }
            set
            {
                if (_currentTheme == value)
                {
                    return;
                }
                _currentTheme = value;
                RaisePropertyChanged();
            }
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            CurrentTheme = ApplicationThemeManager.GetAppTheme();
            AppVersion = $"UiDesktopApp1 - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }


        private ListenerCommand<string> _ChangeThemeCommand;

        public ListenerCommand<string> ChangeThemeCommand
        {
            get
            {
                if (_ChangeThemeCommand == null)
                    _ChangeThemeCommand = new ListenerCommand<string>(ChangeTheme);
                return _ChangeThemeCommand;
            }
        }

        private void ChangeTheme(string parameter)
        {
            var conf = App.GetService<AppConfig>();
            conf.ThemeMode = parameter;
            this.CurrentTheme = conf.GetCurrentTheme();
        }
    }
}

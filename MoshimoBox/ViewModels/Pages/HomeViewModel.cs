using System.Windows.Media;
using Livet;
using Livet.Commands;
using MoshimoBox.Models;
using Windows.Networking.NetworkOperators;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoshimoBox.ViewModels.Pages
{
    public class HomeViewModel : NotificationObject, INavigationAware
    {
        private readonly ISnackbarService _snackbarService;
        public HomeViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
            this.GuidFormats = GuidFormat.GetAll();
            this.SelectedGuidFormat = this.GuidFormats.First();
            this.TimeZones= TimeZoneInfo.GetSystemTimeZones().ToList();
            this.SelectedTimeZone = TimeZoneInfo.Local;
        }

        private bool _isInitialized = false;
        public GuidFormat[] GuidFormats { get; }

        private GuidFormat _SelectedGuidFormat;

        public GuidFormat SelectedGuidFormat
        {
            get
            {
                return _SelectedGuidFormat;
            }
            set
            { 
                if (_SelectedGuidFormat == value)
                {
                    return;
                }
                _SelectedGuidFormat = value;
                GenerateGuidCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public List<TimeZoneInfo> TimeZones { get; } 
        private TimeZoneInfo _SelectedTimeZone;

        public TimeZoneInfo SelectedTimeZone
        {
            get
            {
                return _SelectedTimeZone;
            }
            set
            { 
                if (_SelectedTimeZone == value)
                {
                    return;
                }
                _SelectedTimeZone = value;
                ShowDateTimeCommand.RaiseCanExecuteChanged();
                ShowDateTimeCommand.Execute();
                RaisePropertyChanged();
            }
        }
        private string _DateFormatText = "yyyy/MM/dd HH:mm:ss.fff";

        public string DateFormatText
        {
            get
            {
                return _DateFormatText;
            }
            set
            {
                if (_DateFormatText == value)
                {
                    return;
                }
                _DateFormatText = value;
                ShowDateTimeCommand.RaiseCanExecuteChanged();
                ShowDateTimeCommand.Execute();
                RaisePropertyChanged();
            }
        }


        private ViewModelCommand _ShowDateTimeCommand;

        public ViewModelCommand ShowDateTimeCommand
        {
            get
            {
                if (_ShowDateTimeCommand == null)
                {
                    _ShowDateTimeCommand = new ViewModelCommand(ShowDateTime, CanShowDateTime);
                }
                return _ShowDateTimeCommand;
            }
        }

        public bool CanShowDateTime()
        {
            if (this.SelectedTimeZone == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.DateFormatText))
            {
                return false;
            }
            return true;
        }

        public void ShowDateTime()
        {
            var utcNow = TimeProvider.System.GetUtcNow().UtcDateTime;
            this.NowText = TimeZoneInfo.ConvertTimeFromUtc(utcNow, SelectedTimeZone).ToString(this.DateFormatText);
        }


        private string _NowText;

        public string NowText
        {
            get
            {
                return _NowText;
            }
            private set
            {
                if (_NowText == value)
                {
                    return;
                }
                _NowText = value;
                RaisePropertyChanged();
            }
        }

        private string _EnteredText;
        public string EnteredText
        {
            get
            {
                return _EnteredText;
            }
            set
            { 
                if (_EnteredText == value)
                {
                    return;
                }
                _EnteredText = value;
                this.PascalText = value?.ToPascalCase();
                // this.Base64DecodeText = value?.ToBase64();
                this.UrlEncodeText = value?.UrlEncode();
                this.UrlDecodeText = value?.UrlDeocde();
                this.SHA256Text = value?.ToSha256();
                this.LSnakeText = value?.ToSnakeCase().ToLower();
                this.USnakeText = value?.ToSnakeCase().ToUpper();
                RaisePropertyChanged();
            }
        }

        private string _GuidText;
        public string GuidText
        {
            get
            {
                return _GuidText;
            }
            set
            { 
                if (_GuidText == value)
                {
                    return;
                }
                _GuidText = value;
                RaisePropertyChanged();
            }
        }


        private string _UlidText;

        public string UlidText
        {
            get
            {
                return _UlidText;
            }
            set
            { 
                if (_UlidText == value)
                {
                    return;
                }
                _UlidText = value;
                RaisePropertyChanged();
            }
        }





        private ViewModelCommand _GenerateGuidCommand;
        public ViewModelCommand GenerateGuidCommand
        {
            get
            {
                if (_GenerateGuidCommand == null)
                {
                    _GenerateGuidCommand = new ViewModelCommand(GenerateGuid, CanGenerateGuid);
                }
                return _GenerateGuidCommand;
            }
        }

        private bool CanGenerateGuid()
        {
            return this.SelectedGuidFormat != null;
        }
        public void GenerateGuid()
        {
            var guid = Guid.NewGuid();
            try
            {
                this.GuidText = guid.ToString(SelectedGuidFormat.FormatText);
            }
            catch
            {
                this.GuidText = guid.ToString();
            }
        }


        private ViewModelCommand _GenerateUlidCommand;

        public ViewModelCommand GenerateUlidCommand
        {
            get
            {
                if (_GenerateUlidCommand == null)
                {
                    _GenerateUlidCommand = new ViewModelCommand(GenerateUlid);
                }
                return _GenerateUlidCommand;
            }
        }

        public void GenerateUlid()
        {
            this.UlidText = Ulid.NewUlid().ToString();
        }

        private ListenerCommand<string> _CopyCommand;

        public ListenerCommand<string> CopyCommand
        {
            get
            {
                if (_CopyCommand == null)
                {
                    _CopyCommand = new ListenerCommand<string>(Copy);
                }
                return _CopyCommand;
            }
        }

        public void Copy(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return;
            }
            Clipboard.SetText(parameter);
            OpenSnackBarCommand.Execute("コピーしました");
        }


        private string _PascalText;

        public string PascalText
        {
            get
            {
                return _PascalText;
            }
            set
            { 
                if (_PascalText == value)
                {
                    return;
                }
                _PascalText = value;
                RaisePropertyChanged();
            }
        }


        private string _LSnakeText;

        public string LSnakeText
        {
            get
            {
                return _LSnakeText;
            }
            set
            { 
                if (_LSnakeText == value)
                {
                    return;
                }
                _LSnakeText = value;
                RaisePropertyChanged();
            }
        }


        private string _USnakeText;

        public string USnakeText
        {
            get
            {
                return _USnakeText;
            }
            set
            { 
                if (_USnakeText == value)
                {
                    return;
                }
                _USnakeText = value;
                RaisePropertyChanged();
            }
        }


        private string _SHA256Text;

        public string SHA256Text
        {
            get
            {
                return _SHA256Text;
            }
            set
            { 
                if (_SHA256Text == value)
                {
                    return;
                }
                _SHA256Text = value;
                RaisePropertyChanged();
            }
        }


        private string _Base64EncodeText;

        public string Base64EncodeText
        {
            get
            {
                return _Base64EncodeText;
            }
            set
            { 
                if (_Base64EncodeText == value)
                {
                    return;
                }
                _Base64EncodeText = value;
                RaisePropertyChanged();
            }
        }


        private string _Base64DecodeText;

        public string Base64DecodeText
        {
            get
            {
                return _Base64DecodeText;
            }
            set
            { 
                if (_Base64DecodeText == value)
                {
                    return;
                }
                _Base64DecodeText = value;
                RaisePropertyChanged();
            }
        }


        private string _UrlEncodeText;

        public string UrlEncodeText
        {
            get
            {
                return _UrlEncodeText;
            }
            set
            { 
                if (_UrlEncodeText == value)
                {
                    return;
                }
                _UrlEncodeText = value;
                RaisePropertyChanged();
            }
        }

        private string _UrlDecodeText;

        public string UrlDecodeText
        {
            get
            {
                return _UrlDecodeText;
            }
            set
            { 
                if (_UrlDecodeText == value)
                {
                    return;
                }
                _UrlDecodeText = value;
                RaisePropertyChanged();
            }
        }


        private ListenerCommand<string> _OpenSnackBarCommand;

        public ListenerCommand<string> OpenSnackBarCommand
        {
            get
            {
                if (_OpenSnackBarCommand == null)
                {
                    _OpenSnackBarCommand = new ListenerCommand<string>(OpenSnackBar);
                }
                return _OpenSnackBarCommand;
            }
        }

        public void OpenSnackBar(string parameter)
        {
            _snackbarService.Show(
                "wwww",
                "コピーしました",
                ControlAppearance.Secondary,
                new SymbolIcon(SymbolRegular.Fluent24),
                TimeSpan.FromSeconds(2)
            );
        }


        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }
        private void InitializeViewModel()
        {
            _isInitialized = true;
        }
    }
}

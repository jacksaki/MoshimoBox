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
        }

        private bool _isInitialized = false;
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
    }
}

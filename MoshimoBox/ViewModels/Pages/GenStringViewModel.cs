using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;
using MoshimoBox.Models;
using Wpf.Ui.Controls;
using Wpf.Ui;

namespace MoshimoBox.ViewModels.Pages
{
    public class GenStringViewModel : NotificationObject, INavigationAware
    {
        private readonly ISnackbarService _snackbarService;
        public GuidFormat[] GuidFormats { get; }
        public TimeZoneInfo[] TimeZones { get; }
        public GenStringViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
            this.GuidFormats = GuidFormat.GetAll();
            this.SelectedGuidFormat = this.GuidFormats.First();
            this.TimeZones = TimeZoneInfo.GetSystemTimeZones().ToArray();
            this.SelectedTimeZone = TimeZoneInfo.Local;
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

        private string _GuidText;
        public string GuidText
        {
            get
            {
                return _GuidText;
            }
            private set
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
            private set
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
    }
}

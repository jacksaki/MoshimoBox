using Livet.Commands;
using Livet;
using MoshimoBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using Wpf.Ui;
using Microsoft.Win32;

namespace MoshimoBox.ViewModels.Pages
{
    public class FileViewModel : NotificationObject, INavigationAware
    {
        private readonly ISnackbarService _snackbarService;
        public FileSizeUnit[] Units => FileSizeUnit.GetAll();
        public FileViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
        }

        private string _DateFormat = "yyyy/MM/dd HH:mm:ss";

        public string DateFormat
        {
            get
            {
                return _DateFormat;
            }
            set
            { 
                if (_DateFormat == value)
                {
                    return;
                }
                _DateFormat = value;
                RaisePropertyChanged();
            }
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


        private FileInfo _File;

        public FileInfo File
        {
            get
            {
                return _File;
            }
            set
            { 
                if (_File == value)
                {
                    return;
                }
                if(value == null)
                {
                    _File.PropertyChanged -= _File_PropertyChanged;
                }
                _File = value;
                if (_File != null)
                {
                    _File.PropertyChanged += _File_PropertyChanged;
                }
                RaisePropertyChanged();
            }
        }

        private void _File_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(File));
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


        private ViewModelCommand _OpenFileCommand;

        public ViewModelCommand OpenFileCommand
        {
            get
            {
                if (_OpenFileCommand == null)
                {
                    _OpenFileCommand = new ViewModelCommand(OpenFile);
                }
                return _OpenFileCommand;
            }
        }

        public void OpenFile()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                this.Path = dlg.FileName;
                this.File = new FileInfo(this.Path);
            }
        }
        private string _Path;

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            { 
                if (_Path == value)
                {
                    return;
                }
                _Path = value;
                RaisePropertyChanged();
            }
        }
    }
}
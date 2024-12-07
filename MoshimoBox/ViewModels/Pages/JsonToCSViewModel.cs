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
using System.Windows.Input;
using System.Reflection.Metadata;

namespace MoshimoBox.ViewModels.Pages
{
    public class JsonToCSViewModel : NotificationObject, INavigationAware
    {
        private readonly ISnackbarService _snackbarService;
        public JsonToCSViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
        }

        private void OpenSnackBar(string title,string text,ControlAppearance appearance,SymbolIcon icon)
        {
            _snackbarService.Show(
                title,
                text,
                appearance,
                icon,
                TimeSpan.FromSeconds(2)
            );
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


        private string _JsonText;

        public string JsonText
        {
            get
            {
                return _JsonText;
            }
            set
            { 
                if (_JsonText == value)
                {
                    return;
                }
                _JsonText = value;
                RaisePropertyChanged();
                ConvertToCSCommand.Execute(value);
            }
        }

        private string _CSText;

        public string CSText
        {
            get
            {
                return _CSText;
            }
            private set
            { 
                if (_CSText == value)
                {
                    return;
                }
                _CSText = value;
                RaisePropertyChanged();
            }
        }


        private ListenerCommand<string> _ConvertToCSCommand;

        public ListenerCommand<string> ConvertToCSCommand
        {
            get
            {
                if (_ConvertToCSCommand == null)
                {
                    _ConvertToCSCommand = new ListenerCommand<string>(ConvertToCS, CanConvertToCS);
                }
                return _ConvertToCSCommand;
            }
        }

        public bool CanConvertToCS()
        {
            return !string.IsNullOrEmpty(this.JsonText);
        }

        private CancellationTokenSource _debounceCts;
        public async void ConvertToCS(string parameter)
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(1000, _debounceCts.Token);
                this.CSText = JsonToCSConverter.ConvertJsonToCSharpClass(this.JsonText);
                OpenSnackBar("完了", "変換しました", ControlAppearance.Secondary, new SymbolIcon(SymbolRegular.Fluent24));
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception ex)
            {
                OpenSnackBar("エラー", ex.Message, ControlAppearance.Caution, new SymbolIcon(SymbolRegular.ErrorCircle20));
            }
        }
    }
}
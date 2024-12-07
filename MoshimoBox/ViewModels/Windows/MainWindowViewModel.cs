using System.Collections.ObjectModel;
using Livet;
using MoshimoBox.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoshimoBox.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModel
    {

        private string _ApplicationTitle = "WPF UI - MoshimoBox";
        public ObservableCollection<NavigationViewItem> MenuItems { get; }
        public ObservableCollection<NavigationViewItem> FooterMenuItems { get; }
        public ObservableCollection<MenuItem> TrayMenuItems { get; }

        public string ApplicationTitle
        {
            get
            {
                return _ApplicationTitle;
            }
            set
            {
                if (_ApplicationTitle == value)
                {
                    return;
                }
                _ApplicationTitle = value;
                RaisePropertyChanged();
            }
        }
        public MainWindowViewModel(ISnackbarService snackbarService)
        {
            this.MenuItems = new ObservableCollection<NavigationViewItem>()
            {
                new NavigationViewItem()
                {
                    Content = "String Convert",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                    TargetPageType = typeof(Views.Pages.HomePage)
                },
                new NavigationViewItem()
                {
                    Content = "Generate String",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.AppGeneric24 },
                    TargetPageType = typeof(Views.Pages.GenStringPage)
                },
                new NavigationViewItem()
                {
                    Content = "File",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Document24 },
                    TargetPageType = typeof(Views.Pages.FilePage)
                },
            };
            this.FooterMenuItems = new ObservableCollection<NavigationViewItem>()
            {
                new NavigationViewItem()
                {
                    Content = "Settings",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(Views.Pages.SettingsPage)
                }
            };
            this.TrayMenuItems = new ObservableCollection<MenuItem>()
            {
                new MenuItem { Header = "Home", Tag = "tray_home" }
            };
        }
    }
}

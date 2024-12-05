using MoshimoBox.ViewModels.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace MoshimoBox.Views.Pages
{
    public partial class HomePage : INavigableView<HomeViewModel>
    {
        public HomeViewModel ViewModel { get; }

        public HomePage(HomeViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this.ViewModel;
            InitializeComponent();
        }
    }
}

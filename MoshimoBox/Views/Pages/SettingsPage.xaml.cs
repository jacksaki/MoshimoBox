﻿using MoshimoBox.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace MoshimoBox.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this.ViewModel;

            InitializeComponent();
        }
    }
}

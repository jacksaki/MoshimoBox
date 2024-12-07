﻿using MoshimoBox.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace MoshimoBox.Views.Pages
{
    public partial class FilePage : INavigableView<FileViewModel>
    {
        public FileViewModel ViewModel { get; }

        public FilePage(FileViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this.ViewModel;
            InitializeComponent();
        }
    }
}
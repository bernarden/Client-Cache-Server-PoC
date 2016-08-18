﻿using System;
using System.Windows;
using System.Windows.Threading;

namespace Cache.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 2) };
            timer.Tick += (sender, e) =>
            {
                Content.Height += 10;

                if (Math.Abs(ScrollViewer.VerticalOffset - ScrollViewer.ScrollableHeight) < 2)
                {
                    ScrollViewer.ScrollToEnd();
                }
            };
            timer.Start();
        }

        private void ClearCacheButtonOnClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel model = (MainWindowViewModel)DataContext;
            model.ClearCache();
        }
    }
}

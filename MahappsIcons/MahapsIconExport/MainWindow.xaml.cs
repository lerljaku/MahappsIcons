using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MahapsIconExport
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).Initialize();
        }
    }

    public class IconsResourceDictionary : ResourceDictionary
    {
        public IconsResourceDictionary()
        {
            Source = new IconsUri();
        }
    }

    public class IconsUri : Uri
    {
        public IconsUri() : base("/MahapsIconExport;component/Icons.xaml", UriKind.RelativeOrAbsolute)
        {
        }
    }
}

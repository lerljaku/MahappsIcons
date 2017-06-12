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

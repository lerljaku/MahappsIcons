using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;

namespace MahapsIconExport
{
    public class IconViewModel : Screen
    {
        public IconViewModel(Enum iconKind, string iconKindName, bool isFavourite)
        {
            IconKind = iconKind;
            IsFavourite = isFavourite;
            IconName = iconKind.ToString();
            IconKindName = iconKindName;
        }

        public string IconName { get; set; }

        public Enum IconKind { get; set; }

        public string IconKindName { get; set; }

        private bool m_isFavourite;
        public bool IsFavourite
        {
            get => m_isFavourite;
            set
            {
                m_isFavourite = value;
                NotifyOfPropertyChange(nameof(IsFavourite));
                NotifyOfPropertyChange(nameof(Background));
                NotifyOfPropertyChange(nameof(Foreground));
            }
        }

        public SolidColorBrush Background => !IsFavourite ? (SolidColorBrush)Application.Current.FindResource("WindowBackgroundBrush") : (SolidColorBrush)Application.Current.FindResource("AccentColorBrush");

        public SolidColorBrush Foreground => !IsFavourite ? (SolidColorBrush)Application.Current.FindResource("AccentColorBrush") : (SolidColorBrush)Application.Current.FindResource("WindowBackgroundBrush");
    }
}
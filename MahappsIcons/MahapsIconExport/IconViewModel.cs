using System.Windows.Media;
using Caliburn.Micro;

namespace MahapsIconExport
{
    public class IconViewModel : Screen
    {
        public IconViewModel(object iconCanvas, string iconName, bool isFavourite)
        {
            IconCanvas = iconCanvas;
            IconName = iconName;
            IsFavourite = isFavourite;
        }

        public object IconCanvas { get; set; }

        public string IconName { get; set; }

        private bool m_isFavourite;
        public bool IsFavourite
        {
            get { return m_isFavourite; }
            set
            {
                m_isFavourite = value;
                NotifyOfPropertyChange(nameof(IsFavourite));
                NotifyOfPropertyChange(nameof(Background));
            }
        }

        public SolidColorBrush Background => IsFavourite ? Brushes.LightSeaGreen : Brushes.Transparent;
    }
}
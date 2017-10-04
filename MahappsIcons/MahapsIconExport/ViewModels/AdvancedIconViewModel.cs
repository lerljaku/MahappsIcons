using System;
using Caliburn.Micro;

namespace MahapsIconExport.ViewModels
{
    public class AdvancedIconViewModel : Screen
    {
        public AdvancedIconViewModel(Enum iconKind)
        {
            m_iconWidth = 128;
            m_iconHeight = 128;
            IconKind = iconKind;
        }

        private double m_iconWidth;
        public double IconWidth
        {
            get => m_iconWidth;
            set
            {
                m_iconWidth = value; 
                NotifyOfPropertyChange();
            }
        }

        private double m_iconHeight;
        public double IconHeight
        {
            get => m_iconHeight;
            set
            {
                m_iconHeight = value;
                NotifyOfPropertyChange();
            }
        }

        public Enum IconKind { get; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;

namespace MahapsIconExport
{
    public class MainWindowViewModel : Screen
    {
        private readonly List<IconViewModel> m_allIcons;
        private static readonly string m_favouriteIconsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MahapsIcons");
        private static readonly string m_favouriteIconsPath = Path.Combine(m_favouriteIconsDir, "FavouriteIcons.txt");
        private readonly IconsResourceDictionary m_dict = new IconsResourceDictionary();

        public MainWindowViewModel()
        {
            m_allIcons = new List<IconViewModel>();

            var favouriteIcons = Load();

            foreach (var iconName in m_dict.Keys)
            {
                m_allIcons.Add(new IconViewModel(m_dict[iconName], iconName.ToString(), favouriteIcons.Contains(iconName.ToString())));
            }

            AddToFavouriteCommand = new RelayCommand(AddToFavourite);
        }

        public ICommand AddToFavouriteCommand { get; }

        private string m_filter;
        public string Filter
        {
            get { return m_filter; }
            set
            {
                m_filter = value;

                NotifyOfPropertyChange(nameof(Filter));
                NotifyOfPropertyChange(nameof(Icons));
            }
        }

        private void AddToFavourite(object parameter)
        {
            var casted = (IconViewModel)parameter;
            casted.IsFavourite = !casted.IsFavourite;

            var favouriteIcons = m_allIcons.Where(d => d.IsFavourite).Select(d => d.IconName);

            Save(favouriteIcons);
        }

        public IEnumerable<IconViewModel> Icons => string.IsNullOrEmpty(Filter) ? m_allIcons : m_allIcons.Where(d => d.IconName.Contains(Filter));

        private static void Save(IEnumerable<string> iconNames)
        {
            if (!Directory.Exists(m_favouriteIconsPath))
                Directory.CreateDirectory(m_favouriteIconsDir);

            File.WriteAllLines(m_favouriteIconsPath, iconNames);
        }

        private static IList<string> Load()
        {
            if(!File.Exists(m_favouriteIconsPath))
                return new List<string>();

            return File.ReadAllLines(m_favouriteIconsPath);
        }
    }
}